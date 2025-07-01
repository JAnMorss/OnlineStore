using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.UpdateProductDetails
{
    public sealed record UpdateProductDetailsCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int Stock,
        Guid CategoryId) : ICommand<Guid>;
}
