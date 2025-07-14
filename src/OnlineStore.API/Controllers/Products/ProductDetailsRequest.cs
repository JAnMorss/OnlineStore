namespace OnlineStore.API.Controllers.Products
{
    public sealed record ProductDetailsRequest(
        Guid CategoryId,
        string Name,
        string Description,
        decimal Price);
}
