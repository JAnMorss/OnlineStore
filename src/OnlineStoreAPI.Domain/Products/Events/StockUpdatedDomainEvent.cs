
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class StockUpdatedDomainEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public int Stock { get; }

        public StockUpdatedDomainEvent(Guid productId, int stock)
        {
            ProductId = productId;
            Stock = stock;
        }
    }
}
