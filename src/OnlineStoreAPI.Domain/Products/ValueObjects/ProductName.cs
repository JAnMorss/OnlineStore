using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Products.ValueObjects
{
    public sealed class ProductName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 50;

        public ProductName(string value)
        {
            Value = value;
        }

        public static Result<ProductName> Create(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return Result.Failure<ProductName>(new Error(
                    "Name.Empty", 
                    "Name is empty"));
            }

            if (productName.Length > MaxLength)
            {
                return Result.Failure<ProductName>(new Error(
                    "Name.TooLong",
                    $"Name is too long. Maximum length is {MaxLength} characters."));
            }

            return new ProductName(productName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
