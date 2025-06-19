using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : ICommand<Guid>;
}
