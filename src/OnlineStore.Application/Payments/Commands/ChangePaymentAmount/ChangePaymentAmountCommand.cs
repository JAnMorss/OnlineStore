using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentAmount
{
    public sealed record ChangePaymentAmountCommand(
        Guid PaymentId,
        decimal NewAmount) : ICommand<Guid>;
}
