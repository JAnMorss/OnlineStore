using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed class PaymentAmountChangedDomainEvent : IDomainEvent
    {
        public Guid PaymentId { get; }

        public Money NewAmount { get; }

        public PaymentAmountChangedDomainEvent(Guid paymentId, Money newAmount)
        {
            PaymentId = paymentId;
            NewAmount = newAmount;
        }

    }
}
