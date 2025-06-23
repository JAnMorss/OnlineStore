using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class ProductCreatedDomainEvent(Guid ProductId) : IDomainEvent;
}
