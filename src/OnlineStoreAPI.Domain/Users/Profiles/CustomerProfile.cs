using OnlineStoreAPI.Domain.Users.ValueObjects;

namespace OnlineStoreAPI.Domain.Users.Profiles
{
    public sealed class CustomerProfile
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Address Address { get; private set; }

        public string FullName => $"{FirstName.Value} {LastName.Value}";

        public CustomerProfile(
            FirstName firstName,
            LastName lastName,
            PhoneNumber phoneNumber,
            Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
        }

    }
}
