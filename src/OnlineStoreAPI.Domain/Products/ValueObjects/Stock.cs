using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Products.ValueObjects
{
    public sealed class Stock : ValueObject
    {
        public int Quantity { get; }

        public Stock(int quantity)
        {
            Quantity = quantity;
        }

        public static Result<Stock> Create(int quantity)
        {
            if (quantity < 0)
            {
                return Result.Failure<Stock>(new Error(
                    "Stock.Invalid",
                    "Stock quantity cannot be negative."));
            }
            return new Stock(quantity);
        }

        public Stock Increase(int amount)
        {
            return new Stock(Quantity + amount);
        }

        public Result<Stock> Decrease(int amount)
        {
            if (Quantity - amount < 0)
            {
                return Result.Failure<Stock>(new Error(
                    "Stock.Insufficient",
                    "Not enough stock"));
            }

            return new Stock(Quantity - amount);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Quantity;
        }
    }
}
