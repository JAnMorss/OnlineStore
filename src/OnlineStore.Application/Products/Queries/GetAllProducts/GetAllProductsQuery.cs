using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using OnlineStore.Application.Shared.PageSize;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(QueryObject? Query) : IQuery<PaginatedResult<ProductResponse>>;
}
