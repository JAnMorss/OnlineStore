using System.Text.RegularExpressions;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string Value { get; }

        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>(new Error(
                    "Email.Empty",
                    "Email cannot be empty."));
            }

            email = email.Trim();

            if (!Regex.IsMatch(email, EmailPattern))
            {
                return Result.Failure<Email>(new Error(
                    "Email.InvalidFormat",
                    "Email format is invalid."));
            }

            return new Email(email);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
