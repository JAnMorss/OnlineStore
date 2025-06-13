using OnlineStoreAPI.Domain.Customers.Events;
using OnlineStoreAPI.Domain.Customers.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Customers.Entities
{
    public sealed class Customer : Entity
    {
        private Customer() { }

        private Customer(
            Guid id,
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            Address address) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public Email Email { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Address Address { get; private set; }

        public string FullName => $"{FirstName.Value} {LastName.Value}";

        public static Customer Create(
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            Address address)
        {
            var customer = new Customer(
                Guid.NewGuid(), 
                firstName,
                lastName,
                email,
                phoneNumber,
                address);

            customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(
                    customer.Id,
                    customer.FullName,
                    customer.Email.Value));

            return customer;
        }

        public void UpdateDetails(
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;

            RaiseDomainEvent(new CustomerUpdatedDomainEvent(Id));
        }


    }
}
