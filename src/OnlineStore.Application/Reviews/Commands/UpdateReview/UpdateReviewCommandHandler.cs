using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Reviews.Errors;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Domain.Reviews.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Reviews.Commands.UpdateReview
{
    public sealed class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review is null)
                return Result.Failure(ReviewErrors.NotFound);

            var newRating = Rating.Create(request.Rating);
            if (newRating.IsFailure)
                return Result.Failure(newRating.Error);

            var newComment = Comment.Create(request.Comment);
            if (newComment.IsFailure)
                return Result.Failure(newComment.Error);

            review.Update(newRating.Value, newComment.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
