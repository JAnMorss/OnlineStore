using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Orders.Commands.CancelOrder
{
    public sealed record CancelOrderCommand(Guid OrderId) : ICommand;
}
