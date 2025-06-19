using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.DecreaseProductStock
{
    public sealed record DecreaseProductStockCommand(Guid ProductId, int Quantity) : ICommand<Guid>;

}
