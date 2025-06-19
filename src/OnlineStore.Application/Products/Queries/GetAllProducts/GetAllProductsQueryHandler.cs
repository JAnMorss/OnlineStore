using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Products.DTO_s;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler 
        : IQueryHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAllAsync(cancellationToken);

            var result = product
                .Select(ProductResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
