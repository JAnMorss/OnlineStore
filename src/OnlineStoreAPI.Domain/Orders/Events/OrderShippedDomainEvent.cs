using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Orders.Events
{
    public sealed record OrderShippedDomainEvent(Guid orderId) : IDomainEvent;
}
