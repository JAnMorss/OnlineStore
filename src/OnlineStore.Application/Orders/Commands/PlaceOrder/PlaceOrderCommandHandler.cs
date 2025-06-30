using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.OrderItems.ValueObjects;
using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Products.Errors;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Orders.Commands.PlaceOrder
{
    public sealed class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlaceOrderCommandHandler(
            IOrderRepository orderRepository, 
            IPaymentRepository paymentRepository, 
            IProductRepository productRepository, 
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if(product is null) 
                return Result.Failure<Guid>(ProductErrors.NotFound);

            var quantityResult = Quantity.Create(request.Quantity);
            if (quantityResult.IsFailure)
                return Result.Failure<Guid>(quantityResult.Error);

            var orderItem = new OrderItem(
                Guid.NewGuid(),
                product.Id,
                quantityResult.Value,
                product.Price
            );

            var billing = new Address(
                request.BillingAddress.Street,
                request.BillingAddress.City,
                request.BillingAddress.Barangay,
                request.BillingAddress.ZipCode,
                request.BillingAddress.Country
            );

            var shipping = new Address(
                request.ShippingAddress.Street,
                request.ShippingAddress.City,
                request.ShippingAddress.Barangay,
                request.ShippingAddress.ZipCode,
                request.ShippingAddress.Country
            );

            var order = new Order(
                Guid.NewGuid(),
                request.UserId,
                DateTime.UtcNow,
                request.Currency,
                billing,
                shipping
            );

            order.AddItem(orderItem);

            var paymentMethodResult = PaymentMethod.Create(request.PaymentMethod.Value);
            if (paymentMethodResult.IsFailure)
                return Result.Failure<Guid>(paymentMethodResult.Error);

            var paymentMethod = paymentMethodResult.Value;

            var payment = new Payment(
                Guid.NewGuid(),
                order.Id,
                order.TotalAmount,
                DateTime.UtcNow,
                paymentMethodResult.Value,
                PaymentStatus.Pending
            );

            order.AttachPayment(payment.Id);

            await _orderRepository.AddAsync(order, cancellationToken);
            await _paymentRepository.AddAsync(payment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(order.Id);
        }
    }
}
