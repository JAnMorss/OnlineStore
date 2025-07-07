using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class StoreDescription : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 300;

        public StoreDescription(string value)
        {
            Value = value;
        }

        public static Result<StoreDescription> Create(string description)
        {
            if (description is null)
            {
                return Result.Failure<StoreDescription>(new Error(
                    "StoreDescription.Null",
                    "Store description cannot be null."));
            }

            if (description.Length > MaxLength)
            {
                return Result.Failure<StoreDescription>(new Error(
                    "StoreDescription.TooLong",
                    $"Store description is too long. Maximum length is {MaxLength} characters."));
            }

            return new StoreDescription(description.Trim());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
