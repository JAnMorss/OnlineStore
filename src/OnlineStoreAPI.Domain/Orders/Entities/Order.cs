using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Events;
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
            Guid customerId, 
            DateTime orderDate,
            Currency currency) : base(id)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalAmount = Money.Zero(currency);
        }

        public Guid CustomerId { get; private set; }

        public DateTime OrderDate { get; private set; }

        public Money TotalAmount { get; private set; }


        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyList<OrderItem> Items => _orderItems.AsReadOnly();

        public Result AddItem(OrderItem item)
        {
            if (item is null)
            {
                return Result.Failure(OrderErrors.OrderItemNull);
            }

            _orderItems.Add(item);

            TotalAmount = TotalAmount.Add(item.TotalPrice);

            RaiseDomainEvent(new OrderItemAddedDomainEvent(Id, item.Id));

            return Result.Success();
        }
    }
}
