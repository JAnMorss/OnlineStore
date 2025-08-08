using OnlineStoreAPI.Domain.OrderItems.ValueObjects;
using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.OrderItems.Entities;

public sealed class OrderItem : BaseEntity
{
    private OrderItem() { }

    public OrderItem(
        Guid id,
        Guid orderId,
        Guid productId,
        Quantity quantity,
        Money unitPrice) : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid OrderId { get; private set; } 
    public Order Order { get; private set; }  

    public Guid ProductId { get; private set; }

    public Quantity Quantity { get; private set; }

    public Money UnitPrice { get; private set; }

    public Money TotalPrice => UnitPrice.Multiply(Quantity.Value);
}

