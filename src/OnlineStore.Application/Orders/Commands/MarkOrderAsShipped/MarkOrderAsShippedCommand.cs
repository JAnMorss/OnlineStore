using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Orders.Commands.MarkOrderAsShipped
{
    public record MarkOrderAsShippedCommand(Guid OrderId) : ICommand;
}   
