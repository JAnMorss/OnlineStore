using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(QueryObject? Query) : IQuery<PaginatedResult<ProductResponse>>;
}
