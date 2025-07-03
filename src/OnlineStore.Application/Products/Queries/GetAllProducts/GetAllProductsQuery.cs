using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(
        int Page = 1,
        int PageSize = 10,
        string? SortBy = null,
        bool Descending = false) : IQuery<PaginatedResult<ProductResponse>>;
}
