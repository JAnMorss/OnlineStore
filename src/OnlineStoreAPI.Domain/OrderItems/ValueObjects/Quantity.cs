using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.OrderItems.ValueObjects
{
    public sealed class Quantity : ValueObject
    {
        public int Value { get; }

        private Quantity(int value)
        {
            Value = value;
        }

        public static Result<Quantity> Create(int quantity)
        {
            if (quantity <= 0)
            {
                return Result.Failure<Quantity>(new Error(
                    "Quantity.Invalid",
                    "Quantity must be greater than zero."));
            }

            return Result.Success(new Quantity(quantity));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
