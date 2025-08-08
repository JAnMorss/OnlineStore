using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Reviews.DTOs;
using OnlineStoreAPI.Domain.Reviews.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Reviews.Queries.GetReviewByProduct
{
    public sealed class GetReviewsByProductQueryHandler : IQueryHandler<GetReviewsByProductQuery, List<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsByProductQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<List<ReviewDto>>> Handle(GetReviewsByProductQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

            var result = reviews
                .OrderByDescending(r => r.CreatedOnUtc)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ReviewDto.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
