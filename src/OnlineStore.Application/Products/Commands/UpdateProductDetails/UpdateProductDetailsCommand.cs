using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.UpdateProductDetails
{
    public sealed record UpdateProductDetailsCommand(
        Guid Id,
        Guid CategoryId,
        string Name,
        string Description,
        decimal Price) : ICommand<Guid>;
}
