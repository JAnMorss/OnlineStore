using OnlineStoreAPI.Domain.OrderItems.Entities;

namespace OnlineStore.Application.Orders.DTOs
{
    public sealed class OrderItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }

        public static OrderItemDto FromEntity(OrderItem item)
        {
            return new OrderItemDto
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
