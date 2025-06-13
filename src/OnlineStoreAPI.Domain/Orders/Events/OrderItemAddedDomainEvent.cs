using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Orders.Events
{
    public sealed class OrderItemAddedDomainEvent : IDomainEvent
    {
        public Guid OrderId { get; }

        public Guid OrderItemId { get; }

        public OrderItemAddedDomainEvent(Guid orderId, Guid orderItemId)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
        }
    }
}
