using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Queries.GetPagedCategories
{
    public sealed class GetPagedCategoriesQueryHandler
        : IQueryHandler<GetPagedCategoriesQuery, PagedResult<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;
        public GetPagedCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<CategoryResponse>>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            var (categories, totalCount) = await _repository.GetPagedAsync(
                request.Page,
                request.PageSize,
                cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.NameFilter))
            {
                categories = categories
                    .Where(c => c.Name.Value.Contains(request.NameFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var dtoList = categories
                .Select(c => new CategoryResponse(
                    c.Id,
                    c.Name.Value,
                    c.Description.Value)).ToList();

            return Result.Success(new PagedResult<CategoryResponse>(dtoList, totalCount));
        }
    }
}
