using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Customers.Events
{
    public sealed class CustomerUpdatedDomainEvent : IDomainEvent
    {
        public Guid CustomerId { get; }

        public CustomerUpdatedDomainEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
