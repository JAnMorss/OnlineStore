using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(
        string Name,
        string Description) : ICommand<Guid>;
}
