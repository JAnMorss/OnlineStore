using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using OnlineStore.Application.Shared.PageSize;
using OnlineStore.Application.Categories.Responses;

namespace OnlineStore.Application.Categories.Queries.GetCategories
{
    public sealed class GetAllCategoriesQueryHandler 
        : IQueryHandler<GetAllCategoriesQuery, PaginatedResult<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PaginatedResult<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = request.Query ?? new QueryObject();

            var categories = await _repository.GetAllAsync(query, cancellationToken);

            var mapped = categories
                .Select(CategoryResponse.FromEntity)
                .ToList();

            var totalCount = await _repository.CountAsync(cancellationToken);

            var result = new PaginatedResult<CategoryResponse>(
                mapped,
                totalCount,
                query.Page,
                query.PageSize
            );

            return Result.Success(result);
        }
    }
}
