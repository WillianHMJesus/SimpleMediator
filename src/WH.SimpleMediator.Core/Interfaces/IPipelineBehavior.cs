using System.Threading.Tasks;
using System.Threading;

namespace WH.SimpleMediator
{
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken t = default);

    public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}
