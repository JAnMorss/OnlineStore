using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.UpdateProductPrice
{
    public sealed record UpdateProductPriceCommand(Guid ProductId, decimal NewPrice) : ICommand<Guid>;

}
