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
            var (items, totalCount) = await _repository.GetPagedAsync(
                request.Page,
                request.PageSize,
                request.SortBy,
                request.Descending,
                cancellationToken);

            var paged = items.Select(ProductResponse.FromEntity).ToList();

            return Result.Success(new PagedResult<ProductResponse>(paged, totalCount));
        }
    }
}
