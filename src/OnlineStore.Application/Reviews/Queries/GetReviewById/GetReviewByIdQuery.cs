using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Reviews.DTOs;

namespace OnlineStore.Application.Reviews.Queries.GetReviewById
{
    public sealed record GetReviewByIdQuery(Guid ReviewId) : IQuery<ReviewDto>;
}
