using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.DecreaseProductStock
{
    public sealed record DecreaseProductStockCommand(Guid ProductId, int Quantity) : ICommand<Guid>;

}
