using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Products.Responses;

namespace OnlineStore.Application.Products.Queries.GetProductsByCategory
{
    public sealed record GetProductsByCategoryQuery(Guid CategoryId) : IQuery<List<ProductResponse>>;
}
