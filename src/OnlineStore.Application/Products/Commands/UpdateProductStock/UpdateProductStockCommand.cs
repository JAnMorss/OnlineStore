using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.UpdateProductStock
{
    public sealed record UpdateProductStockCommand(
        Guid ProductId, 
        int NewStock) : ICommand<Guid>;
}
