using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<Guid>;
}
