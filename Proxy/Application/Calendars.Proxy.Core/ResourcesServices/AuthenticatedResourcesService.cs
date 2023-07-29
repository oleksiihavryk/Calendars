using Calendars.Proxy.Core.Exceptions;
using Calendars.Proxy.Core.Options;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core.ResourcesServices;
/// <summary>
///     Service for requesting resources from secure resource server endpoints.
/// </summary>
public class AuthenticatedResourcesService : BaseResourcesService
{
    private readonly string _authUri;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IEnumerable<string> _scopes;

    public AuthenticatedResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<ResourcesServerOptions> resOpts,
        IOptions<AuthenticationServerOptions> authOpts)
        : base(clientFactory, resOpts)
    {
        _clientId = resOpts.Value.ClientId;
        _clientSecret = resOpts.Value.ClientSecret;
        _scopes = resOpts.Value.Scopes;
        _authUri = authOpts.Value.Uri;
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
                Address = _authUri + "/connect/token",
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = string.Join(",", _scopes)
            });

        return token.AccessToken ?? throw new AuthenticationException();
    }
}