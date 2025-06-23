using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Users.Events
{
    public sealed record UserRoleChangedDomainEvent(
        Guid UserId,
        string Role) : IDomainEvent;
}
