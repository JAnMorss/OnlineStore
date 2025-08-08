using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(Guid Id) : ICommand<Guid>;
}
