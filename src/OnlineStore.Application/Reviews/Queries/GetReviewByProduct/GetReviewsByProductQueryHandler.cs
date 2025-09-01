using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Reviews.Responses;

namespace OnlineStore.Application.Reviews.Queries.GetReviewByProduct
{
    public sealed class GetReviewsByProductQueryHandler : IQueryHandler<GetReviewsByProductQuery, List<ReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsByProductQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<List<ReviewResponse>>> Handle(GetReviewsByProductQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

            var result = reviews
                .OrderByDescending(r => r.CreatedOnUtc)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ReviewResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
