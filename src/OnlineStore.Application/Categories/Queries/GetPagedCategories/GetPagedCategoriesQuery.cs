using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetPagedCategories
{
    public record GetPagedCategoriesQuery(
        int Page, 
        int PageSize,
        string? NameFilter) : IQuery<PagedResult<CategoryResponse>>;
}
