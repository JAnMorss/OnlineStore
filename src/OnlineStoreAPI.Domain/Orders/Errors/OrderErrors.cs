using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Orders.Errors
{
    public static class OrderErrors
    {
        public static Error OrderItemNull = new(
            "OrderItem.Null", 
            "Order item cannot be null.");

        public static readonly Error CannotCancelShippedOrDelivered = new(
            "Order.CannotCancel", 
            "Only orders that are pending can be cancelled.");

        public static readonly Error NotFound = new(
            "Order.NotFound", 
            "The specified order was not found.");

        public static readonly Error CannotShipNonPendingOrder = new(
            "Order.CannotShip",
            "Only orders that are pending can be marked as shipped.");
    }
}
