using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Orders.Commands.MarkOrderAsShipped
{
    public sealed record MarkOrderAsShippedCommand(Guid OrderId) : ICommand;
}   
