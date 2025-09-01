using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using OnlineStore.Application.Shared.PageSize;
using OnlineStore.Application.Products.Responses;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(QueryObject? Query) : IQuery<PaginatedResult<ProductResponse>>;
}
