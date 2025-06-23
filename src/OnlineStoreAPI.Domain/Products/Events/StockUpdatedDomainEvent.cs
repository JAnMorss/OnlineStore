using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record StockUpdatedDomainEvent(Guid ProductId, int Stock) : IDomainEvent;
}
