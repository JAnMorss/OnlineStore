using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomainEvent(
        Guid ReviewId,
        int Rating,
        string Comment) : IDomainEvent;
}
