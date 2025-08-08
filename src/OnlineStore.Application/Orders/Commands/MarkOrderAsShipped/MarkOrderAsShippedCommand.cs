using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Orders.Commands.MarkOrderAsShipped
{
    public sealed record MarkOrderAsShippedCommand(Guid OrderId) : ICommand;
}   
