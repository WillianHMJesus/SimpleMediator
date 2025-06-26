using WH.SimpleMediator.Examples.ResourceManagers;

namespace WH.SimpleMediator.Examples.Loggers;

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
