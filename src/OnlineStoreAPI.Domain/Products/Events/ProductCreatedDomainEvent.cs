using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class ProductCreatedDomainEvent : IDomainEvent
    {
        public Guid ProductId { get; }

        public ProductCreatedDomainEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
