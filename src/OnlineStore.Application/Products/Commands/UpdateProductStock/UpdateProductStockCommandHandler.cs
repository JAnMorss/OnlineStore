using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.UpdateProductStock
{
    public sealed class UpdateProductStockCommandHandler : ICommandHandler<UpdateProductStockCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductStockCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                return Result.Failure<Guid>(ProductErrors.NotFound);

            var stockResult = Stock.Create(request.NewStock);
            if(stockResult.IsFailure)
                return Result.Failure<Guid>(stockResult.Error);

            product.UpdateStock(stockResult.Value);

            await _repository.UpdateAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
