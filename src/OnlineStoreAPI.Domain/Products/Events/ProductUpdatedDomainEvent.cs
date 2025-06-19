using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class ProductUpdatedDomainEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public ProductUpdatedDomainEvent(Guid productId)
        {
            ProductId = productId;
        }

    }
}
