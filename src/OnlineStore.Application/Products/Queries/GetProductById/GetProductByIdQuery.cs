using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;
}
