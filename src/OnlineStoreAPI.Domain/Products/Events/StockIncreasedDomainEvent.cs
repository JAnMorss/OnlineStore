using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class StockIncreasedDomainEvent : IDomainEvent
    {

        public Guid ProductId { get; }

        public int NewQuantity { get; }

        public int QuantityIncreasedBy { get; }

        public StockIncreasedDomainEvent(
            Guid productId, 
            int newQuantity, 
            int quantityIncreasedBy)
        {
            ProductId = productId;
            NewQuantity = newQuantity;
            QuantityIncreasedBy = quantityIncreasedBy;
        }
    }
}
