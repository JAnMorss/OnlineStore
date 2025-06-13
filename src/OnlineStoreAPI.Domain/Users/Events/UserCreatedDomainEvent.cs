using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Users.Events
{
    public sealed class UserCreatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserCreatedDomainEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
