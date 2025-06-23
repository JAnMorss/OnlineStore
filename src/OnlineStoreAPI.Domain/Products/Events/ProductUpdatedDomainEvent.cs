using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record ProductUpdatedDomainEvent(Guid ProductId) : IDomainEvent;
}
