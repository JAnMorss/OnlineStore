using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;

namespace OnlineStore.Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryResponse>;
}
