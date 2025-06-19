using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.IncreaseProductStock
{
    public sealed class IncreaseProductStockCommandHandler : ICommandHandler<IncreaseProductStockCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public IncreaseProductStockCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(IncreaseProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null) 
                return Result.Failure<Guid>(ProductErrors.NotFound);

            var result = product.IncreaseStock(request.Quantity);
            if (result is null)
                return Result.Failure<Guid>(result.Error);
                    
            await _repository.UpdateAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
