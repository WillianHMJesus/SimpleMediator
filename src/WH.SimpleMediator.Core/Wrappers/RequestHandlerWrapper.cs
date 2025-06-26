using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WH.SimpleMediator
{
    public abstract class RequestHandlerWrapperBase<TResponse>
    {
        public abstract Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider,
            CancellationToken cancellationToken);
    }

    public class RequestHandlerWrapper<TRequest, TResponse> : RequestHandlerWrapperBase<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            Task<TResponse> Handler(CancellationToken t = default) => serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>()
            .Handle((TRequest)request, t == default ? cancellationToken : t);

            using var scope = serviceProvider.CreateScope();

            return scope.ServiceProvider
                .GetServices<IPipelineBehavior<TRequest, TResponse>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<TResponse>)Handler,
                    (next, pipeline) => (t) => pipeline.Handle((TRequest)request, next, t == default ? cancellationToken : t))();
        }
    }
}
