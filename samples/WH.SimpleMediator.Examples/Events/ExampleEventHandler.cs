using WH.SimpleMediator.Examples.ResourceManagers;

namespace WH.SimpleMediator.Examples.Events;

public sealed class ExampleEventHandler : INotificationHandler<ExampleEvent>
{
    public Task Handle(ExampleEvent notification, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            Result.CreateResponseWithData("Event proccessed successfully"));
    }
}
