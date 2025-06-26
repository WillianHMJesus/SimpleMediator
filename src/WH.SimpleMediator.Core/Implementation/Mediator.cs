using System;
using System.Threading.Tasks;
using System.Threading;

namespace WH.SimpleMediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _provider;

        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var wrapperType = typeof(RequestHandlerWrapper<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var wrapper = (RequestHandlerWrapperBase<TResponse>)Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException();

            return await wrapper.Handle(request, _provider, cancellationToken);
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var wrapperType = typeof(NotificationHandlerWrapper<>).MakeGenericType(notification.GetType());
            var wrapper = (NotificationHandlerWrapperBase)Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException();

            await wrapper.Handle(notification, _provider, cancellationToken);
        }
    }
}
