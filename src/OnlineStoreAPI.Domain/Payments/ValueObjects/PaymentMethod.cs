using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Payments.ValueObjects
{
    public sealed class PaymentMethod : ValueObject
    {
        public string Value { get; }

        public PaymentMethod(string value)
        {
            Value = value;
        }

        public static readonly PaymentMethod CreditCard = new("Credit Card");
        public static readonly PaymentMethod PayPal = new("PayPal");
        public static readonly PaymentMethod GCash = new("GCash");
        public static readonly PaymentMethod COD = new("Cash On Delivery");

        public static Result<PaymentMethod> Create(string paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                return Result.Failure<PaymentMethod>(new Error(
                    "PaymentMethod.Empty",
                    "PaymentMethod cannot be empty"));
            }

            return paymentMethod.Trim().ToLowerInvariant() switch
            {
                "credit card" => CreditCard,
                "paypal" => PayPal,
                "gcash" => GCash,
                "cash on delivery" or "cod" => COD,
                _ => Result.Failure<PaymentMethod>(new Error(
                    "PaymentMethod.Invalid",
                    $"'{paymentMethod}' is not a supported payment method."))
            };
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;

    }
}
