using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Categories.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.Application.Command;
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
            var category = new Category(
                Guid.NewGuid(),
                CategoryName.Create(request.Name).Value,
                CategoryDescription.Create(request.Description).Value);

            await _repository.AddAsync(category, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Id);
        }
    }
}
