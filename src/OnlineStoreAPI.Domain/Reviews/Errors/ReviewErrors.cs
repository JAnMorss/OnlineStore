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
    }
}
