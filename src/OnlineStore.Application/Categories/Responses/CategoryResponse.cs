using OnlineStore.Application.Products.Responses;
using OnlineStoreAPI.Domain.Categories.Entities;

namespace OnlineStore.Application.Categories.Responses
{
    public sealed class CategoryResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }

        public IReadOnlyList<ProductResponse> Products { get; }

        public CategoryResponse(
            Guid id, 
            string name, 
            string description, 
            IReadOnlyList<ProductResponse> products)
        {
            Id = id;
            Name = name;
            Description = description;
            Products = products;
        }

        public static CategoryResponse FromEntity(Category category)
        {
            var productResponses = category.Products
                    .Select(ProductResponse.FromEntity)
                    .ToList();

            return new CategoryResponse(
                category.Id,
                category.Name.Value,
                category.Description.Value,
                productResponses
            );
        }
    }
}
