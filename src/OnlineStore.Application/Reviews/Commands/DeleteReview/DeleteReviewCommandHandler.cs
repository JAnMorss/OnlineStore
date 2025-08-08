using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Reviews.Errors;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Reviews.Commands.DeleteReview
{
    public sealed class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review is null)
                return Result.Failure<Guid>(ReviewErrors.NotFound);

            var success = await _reviewRepository.DeleteAsync(review.Id, cancellationToken);
            if (!success)
                return Result.Failure<Guid>(ReviewErrors.NotFound);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(review.Id);
        }
    }
}
