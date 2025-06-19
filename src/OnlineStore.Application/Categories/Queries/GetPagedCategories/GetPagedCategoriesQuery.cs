using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetPagedCategories
{
    public record GetPagedCategoriesQuery(
        int Page, 
        int PageSize,
        string? SortBy,
        bool Descending) : IQuery<PagedResult<CategoryResponse>>;
}
