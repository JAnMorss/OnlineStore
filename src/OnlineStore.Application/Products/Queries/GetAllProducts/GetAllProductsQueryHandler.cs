using MediatR;
using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Abstractions.PageSize;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStoreAPI.Shared.Kernel.Helpers;

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
            var queryObject = new QueryObject
            {
                SortBy = request.SortBy,
                Descending = request.Descending,
                Page = request.Page,
                PageSize = request.PageSize
            };

            var (items, totalCount) = await _repository.GetPagedAsync(queryObject, cancellationToken);

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



