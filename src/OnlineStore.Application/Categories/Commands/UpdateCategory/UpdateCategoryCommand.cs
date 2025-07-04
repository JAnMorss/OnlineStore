﻿using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        Guid Id,
        string Name,
        string Description) : ICommand<Guid>;
}
