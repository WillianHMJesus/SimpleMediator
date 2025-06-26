namespace WH.SimpleMediator.Examples.Events;

public sealed class ExampleEventHandler : INotificationHandler<ExampleEvent>
{
    public Task Handle(ExampleEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
