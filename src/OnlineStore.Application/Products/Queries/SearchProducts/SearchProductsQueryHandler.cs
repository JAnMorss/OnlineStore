using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Queries.SearchProducts
{
    public sealed class SearchProductsQueryHandler 
        : IQueryHandler<SearchProductsQuery, List<ProductResponse>>
    {
        public readonly IProductRepository _repository;

        public SearchProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductResponse>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.SearchAsync(request.Keyword, request.Page, request.PageSize, cancellationToken);
            if (products == null)
                return Result.Failure<List<ProductResponse>>(ProductErrors.NotFound);

            var result = products
                .Select(ProductResponse.FromEntity)
                .ToList();

            return Result.Success(result);

        }
    }
}
