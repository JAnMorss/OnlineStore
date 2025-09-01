using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Categories.Responses;

namespace OnlineStore.Application.Categories.Queries.GetCategoriesByName
{
    public sealed record GetCategoriesByNameQuery(string Name) : IQuery<List<CategoryResponse>>;
}
