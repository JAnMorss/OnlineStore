using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Reviews.ValueObjects
{
    public sealed class Comment : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 500;

        private Comment(string value)
        {
            Value = value;
        }

        public static Result<Comment> Create(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Result.Failure<Comment>(new Error(
                    "Comment.Empty",
                    "Comment cannot be empty."));
            }

            comment = comment.Trim();

            if (comment.Length > MaxLength)
            {
                return Result.Failure<Comment>(new Error(
                    "Comment.TooLong",
                    $"Comment is too long. Maximum length is {MaxLength} characters."));
            }
            return Result.Success(new Comment(comment));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
