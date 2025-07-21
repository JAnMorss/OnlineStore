using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Categories.Errors;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryHandler 
        : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        public ICategoryRepository _repository;
        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category is null)
            {
                return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);
            }

            var dto = new CategoryResponse(
                category.Id,
                category.Name.Value,
                category.Description.Value,
                new List<ProductResponse>()
            );

            return Result.Success(dto);

        }
    }
}
