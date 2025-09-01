using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.UpdateProductDetails
{
    public sealed record UpdateProductDetailsCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price) : ICommand<Guid>;
}
