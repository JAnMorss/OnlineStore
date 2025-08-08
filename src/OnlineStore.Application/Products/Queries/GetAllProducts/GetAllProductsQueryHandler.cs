using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using OnlineStore.Application.Shared.PageSize;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler
        : IQueryHandler<GetAllProductsQuery, PaginatedResult<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PaginatedResult<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = request.Query ?? new QueryObject();

            var products = await _repository.GetAllAsync(query, cancellationToken);

            var mapped = products.Select(ProductResponse.FromEntity).ToList();

            var totalCount = await _repository.CountAsync(cancellationToken);

            var result = new PaginatedResult<ProductResponse>(
                   mapped,
                   totalCount,
                   query.Page,  
                   query.PageSize);

            return Result.Success(result);
        }
    }
}



