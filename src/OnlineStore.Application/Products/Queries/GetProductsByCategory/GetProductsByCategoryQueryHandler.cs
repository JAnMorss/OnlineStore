using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Products.Responses;

namespace OnlineStore.Application.Products.Queries.GetProductsByCategory
{
    public sealed class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetProductsByCategoryQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetByCategoryIdAsync(request.CategoryId, cancellationToken);
            if (products == null || !products.Any())
                return Result.Failure<List<ProductResponse>>(ProductErrors.NotFound); 

            var result = products
                .Select(ProductResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
