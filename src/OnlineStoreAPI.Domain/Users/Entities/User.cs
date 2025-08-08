using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Domain.Users.Enum;
using OnlineStoreAPI.Domain.Users.Errors;
using OnlineStoreAPI.Domain.Users.Events;
using OnlineStoreAPI.Domain.Users.Profiles;
using OnlineStoreAPI.Domain.Users.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.Entities
{
    public sealed class User : BaseEntity
    {
        private User() { }

        public User(
            Guid id,
            UserName userName,
            EmailVO email,
            Role role) : base(id)
        {
            UserName = userName;
            Email = email;
            Role = role;
            ProfileType = ProfileType.None;
        }

        public UserName UserName { get; private set; }
        public Address Address { get; private set; }
        public EmailVO Email { get; private set; }
        public Role Role { get; private set; }

        public ProfileType ProfileType { get; private set; }

        public CustomerProfile? CustomerProfile { get; private set; }
        public SellerProfile? SellerProfile { get; private set; }


        public static Result<User> Create(
            UserName userName,
            EmailVO email,
            Role role)
        {
            var user = new User(
                Guid.NewGuid(),
                userName,
                email,
                role);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email.Value));

            return Result.Success(user);
        }

        public Result AddCustomerProfile(
            FirstName firstName,
            LastName lastName,
            PhoneNumber phone,
            Address address)
        {
            if (CustomerProfile is not null)
            { 
                return Result.Failure(UserErrors.CustomerProfileExists); 
            }

            CustomerProfile = new CustomerProfile(
                firstName, 
                lastName, 
                phone, 
                address);

            ProfileType |= ProfileType.Customer;

            return Result.Success();
        }

        public Result AddSellerProfile(
            ShopName shopName,
            StoreDescription description,
            PhoneNumber phone,
            Address address)
        {
            if (SellerProfile is not null)
            {
                return Result.Failure(UserErrors.SellerProfileExists);
            }

            SellerProfile = new SellerProfile(
                shopName, 
                description, 
                phone, 
                address);

            ProfileType |= ProfileType.Seller;

            return Result.Success();
        }

        public Result ChangeRole(Role newRole)
        {
            if (Role.Is(newRole))
            { 
                return Result.Failure(UserErrors.UserSameRole); 
            }

            Role = newRole;

            RaiseDomainEvent(new UserRoleChangedDomainEvent(Id, Role.Value));

            return Result.Success();
        }

        public bool IsInRole(Role role) 
            => Role.Is(role);

        public bool IsCustomer 
            => ProfileType.HasFlag(ProfileType.Customer);
        public bool IsSeller 
            => ProfileType.HasFlag(ProfileType.Seller);
    }
}

