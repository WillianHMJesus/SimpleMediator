# WH.SimpleMediator

Simple mediator implementation in .NET

Supports request/response, commands, queries, notifications and events, async with intelligent dispatching via C# generic variance.

Examples in the [wiki](https://github.com/WillianHMJesus/SimpleMediator/wiki).

### Installing WH.SimpleMediator

You should install [WH.SimpleMediator with NuGet](https://www.nuget.org/packages/WH.SimpleMediator):

    Install-Package WH.SimpleMediator

Or via the .NET Core command line interface:

    dotnet add package WH.SimpleMediator

### Registering with `IServiceCollection`

WH.SimpleMediator supports `Microsoft.Extensions.DependencyInjection` directly. To register various WH.SimpleMediator services and handlers:

```
services.AddSimpleMediator(cfg => cfg.RegisterServicesFromAssemblies(typeof(Startup).Assembly));
```

To register behaviors:

```csharp
services.AddSimpleMediator(cfg => {
    cfg.AddPipelineBehavior(typeof(ValidationCommand<,>));
    cfg.AddPipelineBehavior(typeof(LoggerCommand<,>));
});
```

### Request/response

The request/response interface handles both command and query scenarios:

```csharp
public record ExampleCommand(Guid Id, string Message) : IRequest<string> { }
```

```csharp
public record ExampleQuery(Guid Id, string Message) : IRequest<string> { }
```

Next, create a handler:

```csharp
public sealed class ExampleHandler : IRequestHandler<ExampleCommand, string>
{
    public Task<string> Handle(ExampleCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Command proccessed successfully");
    }
}
```

```csharp
public sealed class ExampleHandler : IRequestHandler<ExampleQuery, string>
{
    public Task<string> Handle(ExampleQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Query executed successfully");
    }
}
```

Finally, send a message through the mediator:

```csharp
var response = await mediator.Send(new ExampleCommand(Guid.NewGuid(), "Message"));
Debug.WriteLine(response); // "Command proccessed successfully"
```

```csharp
var response = await mediator.Send(new ExampleQuery(Guid.NewGuid(), "Message"));
Debug.WriteLine(response); // "Query executed successfully"
```

### Notifications

For notifications, first create your notification message:

```csharp
public record ExampleEvent(Guid Id, string Message) : INotification { }
```

Next, create zero or more handlers for your notification:

```csharp
public sealed class ExampleEventHandler : INotificationHandler<ExampleEvent>
{
    public Task Handle(ExampleEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
```

Finally, publish your message via the mediator:

```csharp
await mediator.Publish(new ExampleEvent(Guid.NewGuid(), "Message"));
```

### Behaviors

The behavior interface can be used for executions pre or pos the request handler:

```csharp
public class ValidationCommand<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Implement Validation

        return await next(cancellationToken);
    }
}
```

```csharp
public class LoggerCommand<TRequest, TResponse>(ILogger<LoggerCommand<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start Command");

        var result = await next(cancellationToken);

        logger.LogInformation("End command");

        return result;
    }
}
```
