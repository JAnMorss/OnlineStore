using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record StockDecreasedDomainEvent(
         Guid ProductId,
         int NewStockQuantity) : IDomainEvent;
}
