using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.UpdateProductDetails
{
    public sealed class UpdateProductDetailsCommandHandler : ICommandHandler<UpdateProductDetailsCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductDetailsCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateProductDetailsCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return Result.Failure<Guid>(ProductErrors.NotFound);

            product.UpdateDetails(
                new ProductName(request.Name),
                new ProductDescription(request.Description),
                new Money(request.Price, Currency.Php),
                new Stock(request.Stock));

            await _repository.UpdateAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
