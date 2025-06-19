using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IQuery<List<ProductResponse>>;
}
