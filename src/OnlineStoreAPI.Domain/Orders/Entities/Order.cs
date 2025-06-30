using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.Orders.Enums;
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
            Guid userId,
            DateTime orderDate,
            Currency currency,
            Address billingAddress,
            Address shippingAddress) : base(id)
        {
            UserId = userId;
            OrderDate = orderDate;
            TotalAmount = Money.Zero(currency);
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
        }

        public Guid UserId { get; private set; }

        public Guid? PaymentId { get; private set; }

        public Payment? Payment { get; private set; } 

        public DateTime OrderDate { get; private set; }

        public Money TotalAmount { get; private set; }

        public Address BillingAddress { get; private set; }

        public Address ShippingAddress { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Result AddItem(OrderItem item)
        {
            if (item is null)
                return Result.Failure(OrderErrors.OrderItemNull);

            _orderItems.Add(item);
            TotalAmount = TotalAmount.Add(item.TotalPrice);

            RaiseDomainEvent(new OrderItemAddedDomainEvent(Id, item.Id));

            return Result.Success();
        }

        public void AttachPayment(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Result Cancel()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                return Result.Failure(OrderErrors.CannotCancelShippedOrDelivered);

            Status = OrderStatus.Cancelled;

            RaiseDomainEvent(new OrderCancelledDomainEvent(Id));

            return Result.Success();
        }

        public Result MarkAsShipped()
        {
            if (Status != OrderStatus.Pending)
                return Result.Failure(OrderErrors.CannotShipNonPendingOrder);

            Status = OrderStatus.Shipped;

            RaiseDomainEvent(new OrderShippedDomainEvent(Id));

            return Result.Success();
        }
    }
}
