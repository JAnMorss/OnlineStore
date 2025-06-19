using OnlineStoreAPI.Domain.Categories.Entities;

namespace OnlineStore.Application.Categories.DTOs
{
    public sealed class CategoryResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }

        public CategoryResponse(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static CategoryResponse FromEntity(Category category)
        {
            return new CategoryResponse(
                category.Id,
                category.Name.Value,
                category.Description.Value
            );
        }
    }
}
