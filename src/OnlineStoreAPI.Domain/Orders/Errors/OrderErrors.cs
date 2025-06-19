using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Orders.Errors
{
    public static class OrderErrors
    {
        public static Error OrderItemNull = new(
            "OrderItem.Null", 
            "Order item cannot be null.");
    }
}
