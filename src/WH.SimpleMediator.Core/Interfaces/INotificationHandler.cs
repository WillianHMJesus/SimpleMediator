using System.Threading.Tasks;
using System.Threading;

namespace WH.SimpleMediator
{
    public interface INotificationHandler<TNotification>
        where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
