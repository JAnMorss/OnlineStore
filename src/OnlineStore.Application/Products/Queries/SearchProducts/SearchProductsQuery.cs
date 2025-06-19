using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;

namespace OnlineStore.Application.Products.Queries.SearchProducts
{
    public sealed record SearchProductsQuery(string Keyword, int Page, int PageSize) : IQuery<List<ProductResponse>>;

}
