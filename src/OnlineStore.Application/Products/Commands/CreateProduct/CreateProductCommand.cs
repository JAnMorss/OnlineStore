﻿using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        int Stock,
        Guid CategoryId) : ICommand<Guid>;
}
