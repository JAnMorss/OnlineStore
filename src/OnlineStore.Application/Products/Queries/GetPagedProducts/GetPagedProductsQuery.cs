using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetPagedProducts
{
    public sealed record GetPagedProductsQuery(
        int Page,
        int PageSize,
        string? SortBy,
        bool Descending) : IQuery<PagedResult<ProductResponse>>;
}
