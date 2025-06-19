using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class StockDecreasedDomainEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public int NewStockQuantity { get; }

        public StockDecreasedDomainEvent(Guid productId, int newStockQuantity)
        {
            ProductId = productId;
            NewStockQuantity = newStockQuantity;
        }
    }
}
