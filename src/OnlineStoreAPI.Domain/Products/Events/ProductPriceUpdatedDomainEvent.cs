using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed record ProductPriceUpdatedDomainEvent(
        Guid ProductId,
        decimal NewPriceAmount,
        Currency Currency) : IDomainEvent;

}
