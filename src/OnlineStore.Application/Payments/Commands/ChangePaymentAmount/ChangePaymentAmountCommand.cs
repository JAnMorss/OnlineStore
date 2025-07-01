using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentAmount
{
    public sealed record ChangePaymentAmountCommand(
        Guid PaymentId,
        decimal NewAmount) : ICommand<Guid>;
}
