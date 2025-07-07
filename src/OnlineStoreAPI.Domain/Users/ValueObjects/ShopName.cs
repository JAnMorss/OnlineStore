using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class ShopName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 10;

        public ShopName(string value)
        {
            Value = value;
        }

        public static Result<ShopName> Create(string shopName)
        {
            if (string.IsNullOrWhiteSpace(shopName))
            {
                return Result.Failure<ShopName>(new Error(
                    "ShopName.Empty",
                    "Shop name cannot be empty."));
            }

            if (shopName.Length > MaxLength)
            {
                return Result.Failure<ShopName>(new Error(
                    "ShopName.TooLong",
                    $"Shop name is too long. Maximum length is {MaxLength} characters."));
            }

            return new ShopName(shopName.Trim());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
