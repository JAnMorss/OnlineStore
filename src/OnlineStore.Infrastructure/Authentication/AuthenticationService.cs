using OnlineStore.Application.Abstraction.Authentication;
using OnlineStore.Infrastructure.Authentication.Models;
using OnlineStoreAPI.Domain.Users.Entities;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private const string PasswordCredentialType = "password";
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        HttpClient httpClient,
        IOptions<KeycloakOptions> keycloakOptions,
        ILogger<AuthenticationService> logger)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
        _logger = logger;
    }

    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);
        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };

        var endpoint = $"admin/realms/{_keycloakOptions.Realm}/users";

        _logger.LogInformation("Attempting to register user with email: {Email} to endpoint: {Endpoint}",
            user.Email?.Value, endpoint);

        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, userRepresentationModel, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Keycloak registration failed. Status: {StatusCode}, Error: {Error}, Endpoint: {Endpoint}",
                    response.StatusCode, error, endpoint);
                throw new InvalidOperationException($"Keycloak registration failed: {response.StatusCode} - {error}");
            }

            var identityId = ExtractIdentityIdFromLocationHeader(response);
            _logger.LogInformation("Successfully registered user with identity ID: {IdentityId}", identityId);

            return identityId;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request exception during user registration to endpoint: {Endpoint}", endpoint);
            throw;
        }
    }

    private string ExtractIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";
        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location Header can't be null");
        }

        var usersSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.InvariantCultureIgnoreCase);
        if (usersSegmentValueIndex == -1)
        {
            throw new InvalidOperationException($"Could not find users segment in location header: {locationHeader}");
        }

        var userIdentityId = locationHeader.Substring(usersSegmentValueIndex + usersSegmentName.Length);
        return userIdentityId;
    }
}