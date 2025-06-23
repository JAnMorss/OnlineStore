using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Categories.Events
{
    public sealed record CategoryUpdatedDomainEvent(Guid CategoryId) : IDomainEvent;
}
