using OnlineStoreAPI.Domain.OrderItems.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.OrderItems.Entities
{
    public sealed class OrderItem : Entity
    {
        private OrderItem() { }

        public OrderItem(
            Guid id,
            Guid productId,
            Quantity quantity,
            Money unitPrice) : base(id)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }


        public Guid ProductId { get; private set; }

        public Quantity Quantity { get; private set; }

        public Money UnitPrice { get; private set; }

        public Money TotalPrice => UnitPrice.Multiply(Quantity.Value);

    }
}
