using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Categories.DTOs;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStore.Application.Categories.Queries.GetCategories
{
    public sealed record GetAllCategoriesQuery(QueryObject? Query) : IQuery<PaginatedResult<CategoryResponse>>;
}
