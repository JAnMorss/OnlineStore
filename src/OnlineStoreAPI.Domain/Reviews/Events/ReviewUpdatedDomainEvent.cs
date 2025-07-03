using OnlineStoreAPI.Domain.Reviews.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Reviews.Events
{
    public sealed record ReviewUpdatedDomainEvent(
        Guid ReviewId,
        int Rating,
        string Comment) : IDomainEvent;
}
