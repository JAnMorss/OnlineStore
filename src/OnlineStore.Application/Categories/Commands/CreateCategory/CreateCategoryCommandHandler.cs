using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Categories.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var nameResult = CategoryName.Create(request.Name);
            var descriptionResult = CategoryDescription.Create(request.Description);

            var category = new Category(
                Guid.NewGuid(),
                nameResult.Value,
                descriptionResult.Value);

            await _repository.AddAsync(category, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Id);
        }
    }
}
