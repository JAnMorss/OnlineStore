using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Reviews.Entities;
using OnlineStoreAPI.Domain.Reviews.Errors;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Domain.Reviews.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Reviews.Commands.CreateReview
{
    public sealed class CreateReviewCommandHandler
        : ICommandHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var existingReview = await _reviewRepository.GetByCustomerAndProductAsync(
                request.UserId,
                request.ProductId,
                cancellationToken);

            if (existingReview is not null)
                return Result.Failure<Guid>(ReviewErrors.AlreadyReviewed);

            var rating = Rating.Create(request.Rating);
            if (rating.IsFailure)
                return Result.Failure<Guid>(rating.Error);

            var comment = Comment.Create(request.Comment);
            if (comment.IsFailure)
                return Result.Failure<Guid>(comment.Error);

            var review = Review.Create(
                request.ProductId,
                request.UserId,
                rating.Value,
                comment.Value);

            if (review.IsFailure)
                return Result.Failure<Guid>(review.Error);

            await _reviewRepository.AddAsync(review.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(review.Value.Id);
        }
    }
}
