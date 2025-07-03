using OnlineStoreAPI.Domain.Reviews.Entities;

namespace OnlineStore.Application.Reviews.DTOs
{
    public sealed class ReviewDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid UserId { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; }
        public DateTime CreatedOnUtc { get; init; }

        public static ReviewDto FromEntity(Review review) => new()
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
