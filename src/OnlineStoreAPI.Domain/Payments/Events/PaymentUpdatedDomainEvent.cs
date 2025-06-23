using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed class PaymentUpdatedDomainEvent(Guid PaymentId) : IDomainEvent;
}
