using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Payments.ValueObjects
{
    public sealed class PaymentStatus : ValueObject
    {
        public string Value { get; }

        private PaymentStatus(string value)
        {
            Value = value;
        }
        public static PaymentStatus FromValue(string value) => new(value);

        public static PaymentStatus Pending => new("Pending");
        public static PaymentStatus Completed => new("Completed");
        public static PaymentStatus Failed => new("Failed");

        private static readonly HashSet<string> _allowedValues = new()
        {
            Pending.Value,
            Completed.Value,
            Failed.Value
        };

        public static Result<PaymentStatus> Create(string paymentStatus)
        {
            if (string.IsNullOrEmpty(paymentStatus))
            {
                return Result.Failure<PaymentStatus>(new Error(
                    "PaymentStatus.Empty",
                    "Payment status cannot be empty."));
            }

            if (!_allowedValues.Contains(paymentStatus))
            {
                return Result.Failure<PaymentStatus>(new Error(
                    "PaymentStatus.Invalid",
                    $"'{paymentStatus}' is not a valid payment status."));
            }

            return Result.Success(new PaymentStatus(paymentStatus));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
