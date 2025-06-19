using OnlineStoreAPI.Domain.Reviews.Events;
using OnlineStoreAPI.Domain.Reviews.Errors;
using OnlineStoreAPI.Domain.Reviews.ValueObjects;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Reviews.Entities
{
    public sealed class Review : Entity
    {
        private Review() { }

        private Review(
            Guid id,
            Guid productId,
            Guid customerId,
            Rating rating,
            Comment comment,
            DateTime createdOnUtc) : base(id)
        {
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid ProductId { get; private set; }

        public Guid CustomerId { get; private set; }

        public Rating Rating { get; private set; }

        public Comment Comment { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public static Result<Review> Create(
            Guid productId,
            Guid customerId,
            Rating rating,
            Comment comment)
        {
            if (productId == Guid.Empty || customerId == Guid.Empty)
                return Result.Failure<Review>(ReviewErrors.InvalidIds);

            var review = new Review(
                Guid.NewGuid(),
                productId,
                customerId,
                rating,
                comment,
                DateTime.UtcNow);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(
                review.Id,
                review.Rating.Value,
                review.Comment.Value));

            return Result.Success(review);
        }
    }
}
