using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Reviews.ValueObjects
{
    public sealed class Rating : ValueObject
    {
        public int Value { get; }

        private Rating(int value)
        {
            Value = value;
        }

        public static readonly Rating Excellent = new(10);
        public static readonly Rating Poor = new(1);

        public static Result<Rating> Create(int rating)
        {
            if (rating < 1 || rating > 10)
            {
                return Result.Failure<Rating>(new Error(
                    "Rating.Invalid",
                    "Rating must be between 1 and 10."));
            }

            return new Rating(rating);
        }

        
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
