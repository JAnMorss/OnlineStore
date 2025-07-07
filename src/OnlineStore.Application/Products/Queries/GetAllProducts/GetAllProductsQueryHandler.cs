using MediatR;
using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

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
            var (items, totalCount) = await _repository.GetPagedAsync(
                request.Page,
                request.PageSize,
                request.SortBy,
                request.Descending,
                cancellationToken
            );

            var mapped = items.Select(ProductResponse.FromEntity).ToList();

            var result = new PaginatedResult<ProductResponse>(
                mapped,
                totalCount,
                request.Page,
                request.PageSize);

            return Result.Success(result);
        }
    }
}
