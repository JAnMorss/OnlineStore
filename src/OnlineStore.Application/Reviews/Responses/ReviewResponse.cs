using OnlineStoreAPI.Domain.Reviews.Entities;

namespace OnlineStore.Application.Reviews.Responses
{
    public sealed class ReviewResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid UserId { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; }
        public DateTime CreatedOnUtc { get; init; }

        public static ReviewResponse FromEntity(Review review) => new()
        {
            Id = review.Id,
            ProductId = review.ProductId,
            UserId = review.UserId,
            Rating = review.Rating.Value,
            Comment = review.Comment.Value,
            CreatedOnUtc = review.CreatedOnUtc
        };
    }
}
