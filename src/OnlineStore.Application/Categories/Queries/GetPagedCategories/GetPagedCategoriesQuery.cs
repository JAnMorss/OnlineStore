using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetPagedCategories
{
    public sealed record GetPagedCategoriesQuery(
        int Page, 
        int PageSize,
        string? SortBy,
        bool Descending) : IQuery<PaginatedResult<CategoryResponse>>;
}
