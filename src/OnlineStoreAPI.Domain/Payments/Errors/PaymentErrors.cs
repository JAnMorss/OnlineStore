using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Payments.Errors
{
    public static class PaymentErrors
    {
        public static Error NotFound = new(
            "Payment.NotFound",
            "The payment could not be found.");
    }
}
