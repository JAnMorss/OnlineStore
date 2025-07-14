using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Queries.SearchProducts
{
    public sealed class SearchProductsQueryHandler 
        : IQueryHandler<SearchProductsQuery, PaginatedResult<ProductResponse>>
    {
        public readonly IProductRepository _repository;

        public SearchProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PaginatedResult<ProductResponse>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var queryObject = request.Query;

            var (products, totalCount) = await _repository.SearchAsync(
                queryObject,
                cancellationToken);

            var mapped = products.Select(ProductResponse.FromEntity).ToList();

            var result = new PaginatedResult<ProductResponse>(
                mapped,
                totalCount,
                queryObject.Page,
                queryObject.PageSize);

            return Result.Success(result);

        }
    }
}
