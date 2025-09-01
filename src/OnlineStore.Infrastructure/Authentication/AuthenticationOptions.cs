namespace OnlineStore.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; }

    public string MetadataUrl { get; init; } = string.Empty;

    public bool RequireHttpsMetadata { get; init; }

    public string Issuer { get; set; } = string.Empty;
}
