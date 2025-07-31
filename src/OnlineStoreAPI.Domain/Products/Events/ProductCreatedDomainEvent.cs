using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record ProductCreatedDomainEvent(Guid ProductId) : IDomainEvent;
}
