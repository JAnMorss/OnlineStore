using System.Text.RegularExpressions;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class EmailVO : ValueObject
    {
        public string Value { get; }

        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public EmailVO(string value)
        {
            Value = value;
        }

        public static Result<EmailVO> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<EmailVO>(new Error(
                    "Email.Empty",
                    "Email cannot be empty."));
            }

            email = email.Trim();

            if (!Regex.IsMatch(email, EmailPattern))
            {
                return Result.Failure<EmailVO>(new Error(
                    "Email.InvalidFormat",
                    "Email format is invalid."));
            }

            return new EmailVO(email);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
