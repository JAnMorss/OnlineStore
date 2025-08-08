using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Orders.Commands.AttachPaymentToOrder
{
    public sealed record AttachPaymentToOrderCommand(Guid OrderId, Guid PaymentId) : ICommand<Guid>;
}
