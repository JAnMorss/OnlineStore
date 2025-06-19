using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Shared
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Result<Money> Create(decimal amount, Currency currency)
        {
            if (amount <= 0)
            {
                return Result.Failure<Money>(new Error(
                    "Money.NegativeAmount",
                    "Amount cannot be negative."));
            }

            if (currency is null || currency == Currency.None)
            {
                return Result.Failure<Money>(new Error(
                    "Money.InvalidCurrency",
                    "Currency cannot be null or must be specified."));
            }

            return Result.Success(new Money(
                decimal.Round(amount, 2), 
                currency));
        }

        public static Money Zero(Currency currency) 
            => new(0, currency);

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot add money with different currencies.");
            }

            return new Money(
                Amount + other.Amount, 
                Currency);
        }

        public Money Multiply(decimal factor)
        {
            if (factor < 0)
            {
                throw new InvalidOperationException("Cannot multiply money by a negative number.");
            }

            return new Money(
                decimal.Round(Amount * factor, 2),
                Currency);
        }

        public bool IsZero() => Amount == 0;

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString() => $"{Currency.Symbol}{Amount:N2}";
    }
}
