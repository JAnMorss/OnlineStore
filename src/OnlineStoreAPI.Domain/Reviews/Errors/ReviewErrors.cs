using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Reviews.Errors
{
    public static class ReviewErrors
    {
        public static readonly Error InvalidIds = new(
            "Review.InvalidIds",
            "ProductId and CustomerId must be valid GUIDs.");

        public static readonly Error DuplicateReview = new(
            "Review.Duplicate",
            "Customer has already reviewed this product.");

        public static readonly Error AlreadyReviewed = new(
            "Review.AlreadyReviewed",
            "You have already submitted a review for this product.");

        public static readonly Error NotFound = new(
            "Review.NotFound",
            "Review not found.");

        public static readonly Error InvalidUpdate = new(
            "Review.InvalidUpdate",
            "Cannot update review with the same rating and comment.");

    }
}
