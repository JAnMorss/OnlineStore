using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 20;

        private FirstName(string value)
        {
            Value = value;
        }

        public static Result<FirstName> Create(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<FirstName>(new Error(
                    "FistName.Empty",
                    "FistName is Empty"));
            }

            if (firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(new Error(
                    "FirstName.TooLong",
                    $"FirstName is too long. Maximum length is {MaxLength} characters."));
            }

            return new FirstName(firstName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
