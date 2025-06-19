using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Categories.ValueObjects
{
    public sealed class CategoryDescription : ValueObject
    {
        public string Value { get; }
        public const int MaxLength = 500;

        public CategoryDescription(string value)
        {
            Value = value;
        }

        public static Result<CategoryDescription> Create(string categoryDescription)
        {
            if (string.IsNullOrWhiteSpace(categoryDescription))
            {
                return Result.Failure<CategoryDescription>(new Error(
                    "Description.Empty",
                    "Description is empty"));
            }

            if (categoryDescription.Length > MaxLength)
            {
                return Result.Failure<CategoryDescription>(new Error(
                    "Description.TooLong",
                    $"Description is too long. Maximum length is {MaxLength} characters."));
            }

            return new CategoryDescription(categoryDescription);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
