using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Categories.Events
{
    public sealed class CategoryUpdatedDomainEvent : IDomainEvent
    {
        public Guid CategoryId { get; }
        public CategoryUpdatedDomainEvent(Guid categoryId)
        {
            CategoryId = categoryId;
        }

    }
}
