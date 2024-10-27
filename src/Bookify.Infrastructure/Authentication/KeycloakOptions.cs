namespace Bookify.Infrastructure.Authentication;
public sealed class KeycloakOptions
{
    public string AdminUrl { get; set; }
    public string TokenUrl { get; set; }
    public string AdminId { get; init; }
    public string AdminSecret { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }

}
