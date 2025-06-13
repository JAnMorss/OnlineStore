using OnlineStoreAPI.Domain.Categories.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Categories.Entities
{
    public class Category : Entity
    {
        private Category() { }

        private Category(
            Guid id,
            CategoryName name,
            CategoryDescription description) : base(id) 
        {
            Name = name;
            Description = description;
        }

        public CategoryName Name { get; private set; }
        public CategoryDescription Description { get; private set; }

    }
}
