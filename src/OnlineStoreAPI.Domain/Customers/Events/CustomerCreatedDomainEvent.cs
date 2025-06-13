using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Customers.Events
{
    public sealed class CustomerCreatedDomainEvent : IDomainEvent
    {
        public Guid CustomerId { get; }
        public string FullName { get; }
        public string Email { get; }

        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public CustomerCreatedDomainEvent(
            Guid customerId, 
            string fullName, 
            string email)
        {
            CustomerId = customerId;
            FullName = fullName;
            Email = email;
        }
    }

}
