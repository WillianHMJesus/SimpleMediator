using WH.SimpleMediator.Examples.ResourceManagers;

namespace WH.SimpleMediator.Examples.Commands;

public sealed class ExampleCommandHandler : IRequestHandler<ExampleCommand, Result>
{
    public Task<Result> Handle(ExampleCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            Result.CreateResponseWithData("Command proccessed successfully"));
    }
}
