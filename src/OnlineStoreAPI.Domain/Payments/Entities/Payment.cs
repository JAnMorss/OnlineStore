using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Payments.Events;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Entities
{
    public sealed class Payment : Entity
    {
        private Payment() { }

        public Payment(
            Guid id, 
            Guid orderId,
            Money amount, 
            DateTime paymentDate,
            PaymentMethod paymentMethod) : base(id)
        {
            OrderId = orderId;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
        }

        public Guid OrderId { get; private set; }

        public Money Amount { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public Order? Order { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public void UpdateDetails(
            Money amount,
            DateTime paymentDate,
            PaymentMethod paymentMethod)
        {
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;

            RaiseDomainEvent(new PaymentUpdatedDomainEvent(Id));
        }

        public void ChangeAmount(Money newAmount)
        {
            Amount = newAmount;

            RaiseDomainEvent(new PaymentAmountChangedDomainEvent(Id, newAmount));
        }

        public void ChangePaymentMethod(PaymentMethod newMethod)
        {
            PaymentMethod = newMethod;

            RaiseDomainEvent(new PaymentMethodChangedDomainEvent(Id));
        }
    }
}
