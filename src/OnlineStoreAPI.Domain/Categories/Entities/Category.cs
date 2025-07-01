using OnlineStoreAPI.Domain.Categories.Events;
using OnlineStoreAPI.Domain.Categories.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Categories.Entities
{
    public class Category : Entity
    {
        private Category() { }

        public Category(
            Guid id,
            CategoryName name,
            CategoryDescription description) : base(id) 
        {
            Name = name;
            Description = description;
        }

        public CategoryName Name { get; private set; }
        public CategoryDescription Description { get; private set; }

        public void Update(
            CategoryName name, 
            CategoryDescription description)
        {
            Name = name;
            Description = description;

            RaiseDomainEvent(new CategoryUpdatedDomainEvent(Id)); 
        }

    }
}


