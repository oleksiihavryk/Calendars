using Calendars.Proxy.Core.Exceptions;
using Calendars.Proxy.Core.Options;
using IdentityModel.Client;
using IdentityModel;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core.ResourcesServices;

public class AuthenticatedAuthenticationResourcesService : BaseAuthenticationResourcesService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IEnumerable<string> _scopes;

    public AuthenticatedAuthenticationResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<AuthenticationServerOptions> authOpts)
        : base(clientFactory, authOpts)
    {
        _clientId = authOpts.Value.ClientId;
        _clientSecret = authOpts.Value.ClientSecret;
        _scopes = authOpts.Value.Scopes;
    }

    public override async Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null)
    {
        var token = await GetTokenForResourcesServerAsync();

        var newHeaders = headers ?? new Dictionary<string, string>();
        newHeaders.Add("Authorization", $"Bearer {token}");

        return await base.RequestResourceAsync(method, path, body, newHeaders);
    }
    public virtual async Task<string> GetTokenForResourcesServerAsync()
    {
        var token = await Client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address = BaseUri + "/connect/token",
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = string.Join(",", _scopes)
            });

        return token.AccessToken ?? throw new AuthenticationException();
    }
}