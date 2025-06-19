using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Events;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Orders.Entities
{
    public sealed class Order : Entity
    {
        private Order() { }

        public Order(
            Guid id, 
            Guid paymentId, 
            DateTime orderDate,
            Currency currency) : base(id)
        {
            PaymentId = paymentId;
            OrderDate = orderDate;
            TotalAmount = Money.Zero(currency);
        }

        public Guid UserId { get; private set; }

        public Guid? PaymentId { get; private set; }

        public DateTime OrderDate { get; private set; }

        public Money TotalAmount { get; private set; }

        public Payment? Payment { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        public IReadOnlyList<OrderItem> Items => OrderItems.ToList();

        public Result AddItem(OrderItem item)
        {
            if (item is null)
            {
                return Result.Failure(OrderErrors.OrderItemNull);
            }

            OrderItems.Add(item);

            TotalAmount = TotalAmount.Add(item.TotalPrice);

            RaiseDomainEvent(new OrderItemAddedDomainEvent(Id, item.Id));

            return Result.Success();
        }

        public void AttachPayment(Payment payment)
        {
            Payment = payment;
            PaymentId = payment.Id;
        }
    }
}
