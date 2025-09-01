using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using OnlineStore.Application.Shared.PageSize;
using OnlineStore.Application.Products.Responses;

namespace OnlineStore.Application.Products.Queries.SearchProducts
{
    public sealed record SearchProductsQuery(SearchQueryObject Query) : IQuery<PaginatedResult<ProductResponse>>;

}
