using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed class PaymentUpdatedDomainEvent : IDomainEvent
    {
        public Guid PaymentId { get; }

        public PaymentUpdatedDomainEvent(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
