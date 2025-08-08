using MediatR;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Shared.Kernel.Application.Query;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}