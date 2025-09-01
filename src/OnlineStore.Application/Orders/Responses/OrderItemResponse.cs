using OnlineStoreAPI.Domain.OrderItems.Entities;

namespace OnlineStore.Application.Orders.Responses
{
    public sealed class OrderItemResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }

        public static OrderItemResponse FromEntity(OrderItem item)
        {
            return new OrderItemResponse
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity.Value,
                UnitPrice = item.UnitPrice.Amount,
                TotalPrice = item.TotalPrice.Amount
            };
        }
    }
}
