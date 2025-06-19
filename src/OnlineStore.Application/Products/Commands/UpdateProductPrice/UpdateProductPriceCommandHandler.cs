using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.UpdateProductPrice
{
    public sealed class UpdateProductPriceCommandHandler : ICommandHandler<UpdateProductPriceCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductPriceCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                return Result.Failure<Guid>(ProductErrors.NotFound);

            var result = product.UpdatePrice(new Money(request.NewPrice, Currency.Php));
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await _repository.UpdateAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
