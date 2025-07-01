using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Payments.ValueObjects;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentStatus
{
    public sealed record ChangePaymentStatusCommand(
        Guid PaymentId, 
        PaymentStatus NewStatus) : ICommand<Guid>;
}
