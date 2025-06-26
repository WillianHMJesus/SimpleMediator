using WH.SimpleMediator.Examples.ResourceManagers;

namespace WH.SimpleMediator.Examples.Commands;

public record ExampleCommand(Guid Id, string Message) : IRequest<Result> { }
