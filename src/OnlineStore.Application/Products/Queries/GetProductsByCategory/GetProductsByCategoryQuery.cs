using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetProductsByCategory
{
    public sealed record GetProductsByCategoryQuery(Guid CategoryId) : IQuery<List<ProductResponse>>;
}
