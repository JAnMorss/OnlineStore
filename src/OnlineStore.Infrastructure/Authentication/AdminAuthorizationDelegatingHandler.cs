using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using OnlineStore.Infrastructure.Authentication.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Infrastructure.Authentication;

public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly KeycloakOptions _keycloakOptions;
    private readonly ILogger<AdminAuthorizationDelegatingHandler> _logger;

    public AdminAuthorizationDelegatingHandler(
        IOptions<KeycloakOptions> keycloakOptions,
        ILogger<AdminAuthorizationDelegatingHandler> logger)
    {
        _keycloakOptions = keycloakOptions.Value;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting admin authorization token from: {TokenUrl}", _keycloakOptions.TokenUrl);

            var authorizationToken = await GetAuthorizationToken(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                authorizationToken.AccessToken);

            _logger.LogInformation("Sending request to: {RequestUri} with authorization header", request.RequestUri);

            var httpResponseMessage = await base.SendAsync(request, cancellationToken);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Keycloak Admin API request failed. Status: {StatusCode}, Content: {Content}, RequestUri: {RequestUri}",
                    httpResponseMessage.StatusCode, responseContent, request.RequestUri);
            }

            httpResponseMessage.EnsureSuccessStatusCode();
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AdminAuthorizationDelegatingHandler for request: {RequestUri}", request.RequestUri);
            throw;
        }
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var formData = new Dictionary<string, string>
        {
            ["client_id"] = _keycloakOptions.AdminClientId,
            ["client_secret"] = _keycloakOptions.AdminClientSecret,
            ["grant_type"] = "client_credentials"
        };

        _logger.LogInformation("Requesting admin token with client_id: {ClientId} from: {TokenUrl}",
            _keycloakOptions.AdminClientId, _keycloakOptions.TokenUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_keycloakOptions.TokenUrl))
        {
            Content = new FormUrlEncodedContent(formData)
        };

        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Failed to get admin token. Status: {StatusCode}, Error: {Error}",
                    response.StatusCode, errorContent);
            }

            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            if (token == null)
            {
                throw new ApplicationException("Failed to parse admin token response");
            }

            _logger.LogInformation("Successfully obtained admin token");
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while getting admin token from: {TokenUrl}", _keycloakOptions.TokenUrl);
            throw;
        }
    }
}