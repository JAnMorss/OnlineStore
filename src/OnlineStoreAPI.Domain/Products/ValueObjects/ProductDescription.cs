using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Products.ValueObjects
{
    public sealed class ProductDescription : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 500;

        public ProductDescription(string value)
        {
            Value = value;
        }

        public static Result<ProductDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<ProductDescription>(new Error(
                    "Description.Empty",
                    "Description is empty"));
            }
            if (description.Length > MaxLength)
            {
                return Result.Failure<ProductDescription>(new Error(
                    "Description.TooLong",
                    $"Description is too long. Maximum length is {MaxLength} characters."));
            }
            return new ProductDescription(description);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
