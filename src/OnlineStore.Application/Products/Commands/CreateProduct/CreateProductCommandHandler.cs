﻿using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Categories.Errors;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
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

            await _repository.AddAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(product.Id);
        }


    }
}
