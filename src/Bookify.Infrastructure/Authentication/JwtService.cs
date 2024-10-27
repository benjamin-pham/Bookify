using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication;
internal sealed class JwtService : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure");

    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            KeyValuePair<string, string>[] authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.ClientId),
                new("client_secret", _keycloakOptions.ClientSecret),
                new("grant_type", "password"),
                new("username", email),
                new("password", password),
            };

            FormUrlEncodedContent authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);
            HttpResponseMessage response = await _httpClient.PostAsync(
                "",
                authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            AuthorizationToken authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

            if (authorizationToken is null)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return Result.Success(authorizationToken.AccessToken);
        }
        catch(HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}
