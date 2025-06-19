using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Categories.Errors;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Categories.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category is null)
            {
                return Result.Failure<Guid>(CategoryErrors.NotFound);
            }

            var nameResult = CategoryName.Create(request.Name);
            var descriptionResult = CategoryDescription.Create(request.Description);

            if (nameResult.IsFailure)
                return Result.Failure<Guid>(nameResult.Error);

            if (descriptionResult.IsFailure)
                return Result.Failure<Guid>(descriptionResult.Error);

            category.Update(nameResult.Value, descriptionResult.Value);

            await _repository.UpdateAsync(category, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Id);
        }
    }
}
