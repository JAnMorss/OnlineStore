using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Reviews.Responses;

namespace OnlineStore.Application.Reviews.Queries.GetReviewById
{
    public sealed record GetReviewByIdQuery(Guid ReviewId) : IQuery<ReviewResponse>;
}
