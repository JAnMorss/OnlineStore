using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record StockIncreasedDomainEvent(
        Guid ProductId,
        int NewQuantity,
        int QuantityIncreasedBy) : IDomainEvent;
}
