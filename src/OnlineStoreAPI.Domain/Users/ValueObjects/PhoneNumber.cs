using System.Text.RegularExpressions;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 10;

        public PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result.Failure<PhoneNumber>(new Error(
                    "PhoneNumber.Empty",
                    "Phone number cannot be empty."));
            }

            if (phoneNumber.Length != MaxLength)
            {
                return Result.Failure<PhoneNumber>(new Error(
                    "PhoneNumber.InvalidLength",
                    $"Phone number must be exactly {MaxLength} digits."));
            }

            if (!Regex.IsMatch(phoneNumber, @"^9\d{9}$"))
            {
                return Result.Failure<PhoneNumber>(new Error(
                    "PhoneNumber.InvalidFormat",
                    "Phone number must start with 9 and contain only digits."));
            }

            return new PhoneNumber(phoneNumber);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => $"+63 {Value}";
        
    }
}
