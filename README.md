# WH.SimpleMediator

Simple mediator implementation in .NET

Supports request/response, commands, queries, notifications and events, async with intelligent dispatching via C# generic variance.

### Installing WH.SimpleMediator

You should install [WH.SimpleMediator with NuGet](https://www.nuget.org/packages/WH.SimpleMediator):

    Install-Package WH.SimpleMediator

Or via the .NET Core command line interface:

    dotnet add package WH.SimpleMediator

### Contracts

- `IRequest` (including generic variants)
- `INotification`

### Registering with `IServiceCollection`

WH.SimpleMediator supports `Microsoft.Extensions.DependencyInjection.Abstractions` directly. To register various WH.SimpleMediator services and handlers:

```
services.AddSimpleMediator(cfg => cfg.RegisterServicesFromAssemblies(typeof(Startup).Assembly));
```

This registers:

- `IMediator` as transient
- `IRequestHandler<,>` concrete implementations as transient
- `INotificationHandler<>` concrete implementations as transient

To register behaviors:

```csharp
services.AddSimpleMediator(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
    cfg.AddPipelineBehavior(typeof(GenericBehavior<,>));
    });
```
