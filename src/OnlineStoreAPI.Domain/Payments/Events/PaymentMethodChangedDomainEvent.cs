using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed class PaymentMethodChangedDomainEvent : IDomainEvent
    {

        public Guid PaymentId { get; }

        public PaymentMethodChangedDomainEvent(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
