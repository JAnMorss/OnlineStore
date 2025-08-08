using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.IncreaseProductStock
{
    public sealed record IncreaseProductStockCommand
        (Guid ProductId, 
        int Quantity) : ICommand<Guid>;

}
