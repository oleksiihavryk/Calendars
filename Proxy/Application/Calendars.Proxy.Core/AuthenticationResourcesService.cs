using Calendars.Proxy.Core.Exceptions;
using Calendars.Proxy.Core.Options;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core;
/// <summary>
///     Service for requesting resources from secure resource server endpoints.
/// </summary>
public class AuthenticationResourcesService : BaseResourcesService
{
    private readonly string _uri;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IEnumerable<string> _scopes;

    public AuthenticationResourcesService(
        IHttpClientFactory clientFactory, 
        IOptions<ResourcesServerOptions> resOpts,
        IOptions<AuthenticationServerOptions> authOpts) 
        : base(clientFactory, resOpts)
    {
        _clientId = authOpts.Value.ClientId;
        _clientSecret = authOpts.Value.ClientSecret;
        _scopes = authOpts.Value.Scopes;
        _uri = authOpts.Value.Uri;
    }

    public override async Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path = null, 
        object? body = null, 
        IDictionary<string, string>? headers = null)
    {
        var token = GetTokenForResourcesServerAsync();

        var newHeaders = headers ?? new Dictionary<string, string>();
        newHeaders.Add("Authentication", $"Bearer {token}");
        
        return await base.RequestResourceAsync(method, path, body, newHeaders);
    }
    public virtual async Task<string> GetTokenForResourcesServerAsync()
    {
        var token = await Client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address = _uri + "/connect/token",
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = string.Join(",", _scopes)
            });

        return token.AccessToken ?? throw new AuthenticationException(); 
    }
}