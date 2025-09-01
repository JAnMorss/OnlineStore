namespace OnlineStore.Application.Payments.Responses
{
    public sealed class PaymentResponse 
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;

        public static PaymentResponse FromEntity(OnlineStoreAPI.Domain.Payments.Entities.Payment payment)
        {
            return new PaymentResponse
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount.Amount,
                Currency = payment.Amount.Currency.Code,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod.ToString(),
                PaymentStatus = payment.PaymentStatus.ToString()
            };
        }
    }
}
