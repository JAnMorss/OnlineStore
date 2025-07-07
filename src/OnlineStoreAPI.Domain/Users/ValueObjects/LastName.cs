using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 15;

        public LastName(string value)
        {
            Value = value;
        }

        public static Result<LastName> Create(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<LastName>(new Error(
                    "LastName.Empty",
                    "LastName is Empty"));
            }

            if (lastName.Length > MaxLength)
            {
                return Result.Failure<LastName>(new Error(
                    "LastName.TooLong",
                    $"LastName is too long. Maximum length is {MaxLength} characters."));
            }

            return new LastName(lastName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
