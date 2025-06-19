using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetCategories
{
    public record GetAllCategoriesQuery() : IQuery<List<CategoryResponse>>;
}
