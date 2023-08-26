using System.Security.Authentication;
using Calendars.Proxy.Core.Interfaces;
using IdentityModel;
using IdentityModel.Client;

namespace Calendars.Proxy.Core.Services;
/// <summary>
///     Implementation of service decorator for requesting secure resources from external server.
/// </summary>
public class BearerAuthenticationService : ServiceDecorator
{
    private readonly Credentials _credentials;

    public BearerAuthenticationService(
        Credentials credentials,
        IService service) 
        : base(service)
    {
        ArgumentNullException.ThrowIfNull(credentials);

        _credentials = credentials;
    }

    public override async Task<HttpResponseMessage> RequestAsync(
        HttpMethod method, 
        string? path = null, 
        object? body = null, 
        IDictionary<string, string>? headers = null)
    {
        var token = await GetTokenForResourcesServerAsync();

        var newHeaders = headers ?? new Dictionary<string, string>();
        newHeaders.Add("Authorization", $"Bearer {token}");

        return await Service.RequestAsync(method, path, body, newHeaders);
    }

    protected virtual async Task<string> GetTokenForResourcesServerAsync()
    {
        var token = await Client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address = _credentials.AuthenticationServer + "connect/token",
                ClientId = _credentials.ClientId,
                ClientSecret = _credentials.ClientSecret,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = string.Join(",", _credentials.Scopes)
            });

        return token.AccessToken ?? throw new AuthenticationException();
    }
}