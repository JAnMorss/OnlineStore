using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Categories.Errors;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.Id, cancellationToken);

            if (!exists)
            {
                return Result.Failure<Guid>(CategoryErrors.NotFound);
            }

            await _repository.DeleteAsync(request.Id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(request.Id);
        }
    }
}
