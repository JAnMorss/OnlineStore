using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;
}
