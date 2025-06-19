using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Products.Events
{
    public sealed class ProductPriceUpdatedDomainEvent : IDomainEvent
    {
        public Guid Id { get; }

        public decimal NewPriceAmount { get; }

        public Currency Currency { get; }

        public ProductPriceUpdatedDomainEvent(
            Guid id, 
            decimal newPriceAmount, 
            Currency currency)
        {
            Id = id;
            NewPriceAmount = newPriceAmount;
            Currency = currency;
        }

    }
}
