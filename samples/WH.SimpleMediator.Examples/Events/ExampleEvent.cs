namespace WH.SimpleMediator.Examples.Events;

public record ExampleEvent(Guid Id, string Message) : INotification { }
