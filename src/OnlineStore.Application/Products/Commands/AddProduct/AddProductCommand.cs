using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Products.Commands.AddProduct;

public sealed record AddProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId) : ICommand<Guid>;

