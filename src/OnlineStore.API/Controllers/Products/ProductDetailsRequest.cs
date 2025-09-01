namespace OnlineStore.API.Controllers.Products;

public sealed record ProductDetailsRequest(
        string Name,
        string Description,
        decimal Price);