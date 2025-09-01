using OnlineStoreAPI.Domain.Users.Errors;
using OnlineStoreAPI.Domain.Users.Events;
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
            FirstName firstName,
            LastName lastName,
            EmailVO email,
            PhoneNumber phoneNumber) : base(id)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public UserName UserName { get; private set; }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public EmailVO Email { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public string IdentityId { get; private set; } = string.Empty;

        public static Result<User> Create(
                UserName userName,
                FirstName firstName,
                LastName lastName,
                EmailVO email,
                PhoneNumber phoneNumber)
        {
            var user = new User(
                Guid.NewGuid(),
                userName,
                firstName,
                lastName,
                email,
                phoneNumber);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email.Value));

            return Result.Success(user);
        }

        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
        }
    }
}
