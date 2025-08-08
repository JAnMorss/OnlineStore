using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Reviews.DTOs;

namespace OnlineStore.Application.Reviews.Queries.GetReviewByProduct
{
    public sealed record GetReviewsByProductQuery(
        Guid ProductId,
        int PageNumber = 1,
        int PageSize = 10) : IQuery<List<ReviewDto>>;
}
