using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId) : ICommand;
}
