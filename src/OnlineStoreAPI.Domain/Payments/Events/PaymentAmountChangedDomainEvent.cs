using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed record PaymentAmountChangedDomainEvent(
        Guid PaymentId,
        Money NewAmount) : IDomainEvent;
}
