using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Users.Events
{
    public sealed class UserRoleChangedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; }
        public string Role { get; }
        public UserRoleChangedDomainEvent(Guid userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }
}
