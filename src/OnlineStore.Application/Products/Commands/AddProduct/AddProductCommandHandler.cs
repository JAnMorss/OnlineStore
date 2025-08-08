using OnlineStoreAPI.Domain.Categories.Errors;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStore.Application.Abstractions.Messaging;
using MediatR;

namespace OnlineStore.Application.Products.Commands.AddProduct
{
    public sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _Productrepository;
        private readonly ICategoryRepository _categRepository;
        private readonly IUnitOfWork _unitOfWorks;

        public AddProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _Productrepository = repository;
            _unitOfWorks = unitOfWork;
            _categRepository = categoryRepository;
        }

        public async Task<Result<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categRepository.GetByIdAsync(request.CategoryId, cancellationToken);
            if (category is null)
                return Result.Failure<Guid>(CategoryErrors.NotFound);

            var productResult = Product.Create(
                category,
                new ProductName(request.Name),
                new ProductDescription(request.Description),
                request.Price,
                Currency.Php,
                new Stock(request.Stock));

            if (productResult.IsFailure)
                return Result.Failure<Guid>(productResult.Error);

            var product = productResult.Value;

            await _Productrepository.AddAsync(product, cancellationToken);

            await _unitOfWorks.SaveChangesAsync();

            return Result.Success(product.Id);
        }
    }
}
