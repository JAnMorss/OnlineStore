using MediatR;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}
