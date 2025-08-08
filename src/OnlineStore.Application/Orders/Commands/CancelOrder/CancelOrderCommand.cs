using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Orders.Commands.CancelOrder
{
    public sealed record CancelOrderCommand(Guid OrderId) : ICommand;
}
