using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetCategoriesByName
{
    public sealed record GetCategoriesByNameQuery(string Name) : IQuery<List<CategoryResponse>>;
}
