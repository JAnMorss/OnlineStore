using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Payments.ValueObjects;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentMethod
{
    public sealed record ChangePaymentMethodCommand(
        Guid PaymentId,
        PaymentMethod NewMethod) : ICommand<Guid>;
}
