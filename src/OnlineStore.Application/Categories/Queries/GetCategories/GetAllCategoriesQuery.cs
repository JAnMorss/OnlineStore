using OnlineStore.Application.Categories.Responses;
using OnlineStore.Application.Shared.PageSize;
using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStore.Application.Categories.Queries.GetCategories
{
    public sealed record GetAllCategoriesQuery(QueryObject? Query) : IQuery<PaginatedResult<CategoryResponse>>;
}
