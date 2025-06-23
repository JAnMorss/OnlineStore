using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Orders.Events
{
    public sealed record OrderItemAddedDomainEvent(Guid OrderId, Guid OrderItemId) : IDomainEvent;

}
