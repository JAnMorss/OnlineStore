using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.IncreaseProductStock
{
    public sealed record IncreaseProductStockCommand
        (Guid ProductId, 
        int Quantity) : ICommand<Guid>;

}
