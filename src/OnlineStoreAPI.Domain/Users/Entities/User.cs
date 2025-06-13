using OnlineStoreAPI.Domain.Users.Events;
using OnlineStoreAPI.Domain.Users.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Users.Entities
{
    public sealed class User : Entity
    {
        private User() { }

        public User(
            Guid id,
            FullName fullName,
            Email email,
            UserName userName,
            PasswordHash passwordHash,
            Role role) : base(id)
        {
            FullName = fullName;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
            Role = role;
        }

        public FullName FullName { get; private set; }

        public Email Email { get; private set; }

        public UserName UserName { get; private set; }

        public PasswordHash PasswordHash { get; private set; }

        public Role Role { get; private set; }

        public string IdentityId { get; private set; } = string.Empty;

        public static User Create(
            FullName fullName,
            Email email,
            UserName userName,
            PasswordHash passwordHash,
            Role role)
        {
            var user = new User(
                Guid.NewGuid(),
                fullName,
                email,
                userName,
                passwordHash,
                role);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email.Value));

            return user;
        }

        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
        }

        public bool IsInRole(Role role) => Role.Is(role);

        public void ChangeRole(Role newRole)
        {
            Role = newRole;

            RaiseDomainEvent(new UserRoleChangedDomainEvent(Id, Role.Value));

        }
    }
}
