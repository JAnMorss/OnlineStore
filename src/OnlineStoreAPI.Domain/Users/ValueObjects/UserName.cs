using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class UserName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 30;

        private UserName(string value)
        {
            Value = value;
        }

        public static Result<UserName> Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result.Failure<UserName>(new Error(
                    "UserName.Empty",
                    "User name cannot be empty."));
            }

            if (userName.Length > MaxLength)
            {
                return Result.Failure<UserName>(new Error(
                    "UserName.TooLong",
                    $"User name is too long. Maximum length is {MaxLength} characters."));
            }

            return new UserName(userName.Trim());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
