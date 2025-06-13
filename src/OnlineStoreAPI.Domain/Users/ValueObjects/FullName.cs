using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class FullName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 30;

        private FullName(string value)
        {
            Value = value;
        }

        public static Result<FullName> Create(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Result.Failure<FullName>(new Error(
                    "FullName.Empty",
                    "Full name cannot be empty."));
            }

            if (fullName.Length > MaxLength)
            {
                return Result.Failure<FullName>(new Error(
                    "FullName.TooLong",
                    $"Full name is too long. Maximum length is {MaxLength} characters."));
            }

            return new FullName(fullName.Trim());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

    }
}
