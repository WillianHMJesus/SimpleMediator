using WH.SimpleMediator.Examples.ResourceManagers;

namespace WH.SimpleMediator.Examples.Validations;

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