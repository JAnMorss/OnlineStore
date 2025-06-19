using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(
        string Name,
        string Description) : ICommand<Guid>;
}
