using OnlineStoreAPI.Domain.Orders.Entities;

namespace OnlineStore.Application.Orders.DTOs
{
    public sealed class OrderDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid? PaymentId { get; init; }
        public DateTime OrderDate { get; init; }
        public string Currency { get; init; }
        public decimal TotalAmount { get; init; }
        public string BillingAddress { get; init; }
        public string ShippingAddress { get; init; }
        public string Status { get; init; }
        public IReadOnlyList<OrderItemDto> Items { get; init; } = [];

        public static OrderDto FromEntity(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                PaymentId = order.Payment?.Id,
                OrderDate = order.OrderDate,
                Currency = order.TotalAmount.Currency.Code,
                TotalAmount = order.TotalAmount.Amount,
                BillingAddress = order.BillingAddress.ToString(),
                ShippingAddress = order.ShippingAddress.ToString(),
                Status = order.Status.ToString(),
                Items = order.OrderItems
                     .Select(OrderItemDto.FromEntity)
                     .ToList()
            };
        }
    }
}
