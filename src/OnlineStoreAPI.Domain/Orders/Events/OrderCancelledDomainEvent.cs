using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Orders.Events
{
    public sealed record OrderCancelledDomainEvent(Guid orderId) : IDomainEvent;
}
