using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryResponse>;
}
