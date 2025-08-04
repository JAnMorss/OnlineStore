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
            PaymentMethod paymentMethod,
            PaymentStatus paymentStatus) : base(id)
        {
            OrderId = orderId;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            PaymentStatus = paymentStatus;
        }

        public Guid OrderId { get; private set; }

        public Order? Order { get; private set; }

        public Money Amount { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;

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

        public void ChangePaymentStatus(PaymentStatus newStatus)
        {
            PaymentStatus = newStatus;
        }
    }
}


