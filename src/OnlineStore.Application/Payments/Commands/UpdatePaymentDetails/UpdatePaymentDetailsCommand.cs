using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Payments.ValueObjects;

namespace OnlineStore.Application.Payments.Commands.UpdatePaymentDetails
{
    public sealed record UpdatePaymentDetailsCommand(
        Guid PaymentId,
        decimal Amount,
        DateTime PaymentDate,
        PaymentMethod PaymentMethod) : ICommand<Guid>; 
}
