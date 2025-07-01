using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Orders.Commands.AttachPaymentToOrder
{
    public sealed record AttachPaymentToOrderCommand(Guid OrderId, Guid PaymentId) : ICommand<Guid>;
}
