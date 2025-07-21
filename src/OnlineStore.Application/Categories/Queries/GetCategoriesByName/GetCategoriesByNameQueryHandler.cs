using System.Linq;
using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Categories.DTOs;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Queries.GetCategoriesByName
{
    public sealed class GetCategoriesByNameQueryHandler
        : IQueryHandler<GetCategoriesByNameQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _repository;
        public GetCategoriesByNameQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetCategoriesByNameQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByNameAsync(request.Name, cancellationToken);

            var categoryDtos = categories
                .Select(c => new CategoryResponse(
                    c.Id,
                    c.Name.Value,
                    c.Description.Value,
                    new List<ProductResponse>()
                )).ToList();

            return Result.Success(categoryDtos);
        }
    }
}
