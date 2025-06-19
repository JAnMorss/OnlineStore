using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Categories.ValueObjects
{
    public sealed class CategoryName : ValueObject
    {
        public string Value { get; }
        public const int MaxLength = 30;

        public CategoryName(string value)
        {
            Value = value;
        }

        public static Result<CategoryName> Create(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return Result.Failure<CategoryName>(new Error(
                    "Name.Empty",
                    "Name is Empty"));
            }

            if (categoryName.Length > MaxLength)
            {
                return Result.Failure<CategoryName>(new Error(
                    "Name.TooLong",
                    $"Name is too long. Maximum length is {MaxLength} characters."));
            }

            return new CategoryName(categoryName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
