using OnlineStoreAPI.Domain.Products.Entities;

namespace OnlineStore.Application.Products.DTO_s
{
    public sealed class ProductResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public Guid CategoryId { get; init; }

        public static ProductResponse FromEntity(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name.Value,
                Description = product.Description.Value,
                Price = product.Price.Amount,
                Stock = product.Stock.Quantity,
                CategoryId = product.CategoryId
            };
        }
    }

}
