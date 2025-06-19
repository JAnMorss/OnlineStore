using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.DecreaseProductStock
{
    public sealed class DecreaseProductStockCommandHandler : ICommandHandler<DecreaseProductStockCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DecreaseProductStockCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DecreaseProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                return Result.Failure<Guid>(ProductErrors.NotFound);

            var result = product.DecreaseStock(request.Quantity);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await _repository.UpdateAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
