using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Queries.GetPagedProducts
{
    public sealed class GetPagedProductsQueryHandler 
        : IQueryHandler<GetPagedProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetPagedProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<ProductResponse>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var allProducts = await _repository.GetAllAsync(cancellationToken);
            var totalCount = allProducts.Count();

            var sorted = request.SortBy?.ToLower() switch
            {
                "price" => request.Descending
                    ? allProducts.OrderByDescending(p => p.Price.Amount)
                    : allProducts.OrderBy(p => p.Price.Amount),
                "name" => request.Descending
                    ? allProducts.OrderByDescending(p => p.Name.Value)
                    : allProducts.OrderBy(p => p.Name.Value),
                _ => allProducts
            };

            var paged = sorted
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ProductResponse.FromEntity)
                .ToList();

            return Result.Success(new PagedResult<ProductResponse>(paged, totalCount));
        }
    }
}
