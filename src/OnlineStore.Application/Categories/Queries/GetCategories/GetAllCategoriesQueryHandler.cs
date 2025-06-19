using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Queries.GetCategories
{
    public sealed class GetAllCategoriesQueryHandler 
        : IQueryHandler<GetAllCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync(cancellationToken);

            var result = categories
                .Select(CategoryResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
