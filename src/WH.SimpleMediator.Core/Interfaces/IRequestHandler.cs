using System.Threading.Tasks;
using System.Threading;

namespace WH.SimpleMediator;

public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
