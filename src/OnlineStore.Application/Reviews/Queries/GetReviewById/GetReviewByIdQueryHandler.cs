using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Reviews.DTOs;
using OnlineStoreAPI.Domain.Reviews.Errors;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Reviews.Queries.GetReviewById
{
    public sealed class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review is null)
                return Result.Failure<ReviewDto>(ReviewErrors.NotFound);

            var result = ReviewDto.FromEntity(review);

            return Result.Success(result);
        }
    }
}
