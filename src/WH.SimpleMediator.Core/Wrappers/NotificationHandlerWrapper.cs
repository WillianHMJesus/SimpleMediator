using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WH.SimpleMediator
{
    public abstract class NotificationHandlerWrapperBase
    {
        public abstract Task Handle(INotification notification, IServiceProvider serviceProvider,
            CancellationToken cancellationToken);
    }

    public class NotificationHandlerWrapper<TNotification> : NotificationHandlerWrapperBase
        where TNotification : INotification
    {
        public async override Task Handle(INotification notification, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var handlers = serviceProvider.GetServices<INotificationHandler<TNotification>>();
            var tasks = handlers.Select(x => x.Handle((TNotification)notification, cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
