using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Reviews.Events
{
    public sealed class ReviewCreatedDomainEvent : IDomainEvent
    {
        public Guid ReviewId { get; }
        public int Rating { get; }
        public string Comment { get; }

        public ReviewCreatedDomainEvent(Guid reviewId, int rating, string comment)
        {
            ReviewId = reviewId;
            Rating = rating;
            Comment = comment;
        }
    }
}
