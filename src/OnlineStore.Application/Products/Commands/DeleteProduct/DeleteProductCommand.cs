using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<Guid>;
}
