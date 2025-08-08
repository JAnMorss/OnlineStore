using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        Guid Id,
        string Name,
        string Description) : ICommand<Guid>;
}
