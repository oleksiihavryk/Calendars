using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Calendars.Authentication.Core;
/// <summary>
///     In memory configuration of IdentityServer4 framework.
///     All clients, scopes and resources of authentication server.
/// </summary>
public class IdentityServerInMemoryConfiguration
{
    private readonly ClientsConfiguration _clientsConfiguration;
    private readonly bool _isDevelopment;
    
    private string[]? _origins = null; 

    public List<Client> Clients => new()
    {
        //Resources API client.
        new Client()
        {
            ClientId = _clientsConfiguration.Resources.Id,
            ClientName = _clientsConfiguration.Resources.Name,
            ClientSecrets = new[]
            {
                new Secret { Value = _clientsConfiguration.Resources.Secret.ToSha256() }
            },

            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = _clientsConfiguration.Resources.Scopes,

            RedirectUris = _clientsConfiguration.Web.Origins
                .Select(o => new []
                {
                    o + "oauth2-redirect.html",
                    o + "signin-oidc"
                })
                .SelectMany(o => o)
                .ToArray(),
            PostLogoutRedirectUris = _clientsConfiguration.Resources.Origins
                .Select(o => o + "signout-callback-oidc")
                .ToArray(),

            AlwaysIncludeUserClaimsInIdToken = true,
            RequirePkce = _isDevelopment == false,

            AllowedCorsOrigins = ClientsOrigins
        },
        //Identity application client.
        new Client
        {
            ClientId = _clientsConfiguration.Web.Id,
            ClientName = _clientsConfiguration.Web.Name,
            ClientSecrets = new[]
            {
                new Secret(_clientsConfiguration.Web.Secret.ToSha256())
            },

            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes = _clientsConfiguration.Web.Scopes,

            RedirectUris = _clientsConfiguration.Web.Origins
                .Select(o => new []
                {
                    o + "oauth2-redirect.html",
                    o.OriginalString
                })
                .SelectMany(o => o)
                .ToArray(),
            PostLogoutRedirectUris = _clientsConfiguration.Web.Origins
                .Select(o => o.OriginalString)
                .ToArray(),

            RequirePkce = true,
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowAccessTokensViaBrowser = true,

            AllowedCorsOrigins = ClientsOrigins,
        }
    };
    public List<ApiScope> Scopes => new()
    {
        new ApiScope("resources")
    };
    public List<IdentityResource> Resources => new()
    {
        new IdentityResources.OpenId(),
        new IdentityResource()
        {
            Name = IdentityServerConstants.StandardScopes.Email,
            Description = "Email",
            DisplayName = "Email",
            Required = false,
            UserClaims =
            {
                JwtClaimTypes.Email
            }
        },
        new IdentityResource
        {
            Name = IdentityServerConstants.StandardScopes.Profile,
            UserClaims =
            {
                JwtClaimTypes.Name
            },
            Required = true,
            DisplayName = "Profile",
            Description = "Profile data."
        }
    };
    public List<ApiResource> ApiResources => new()
    {
        new ApiResource("resources")
        {
            Scopes = { "resources" }
        }
    };

    public string[] ClientsOrigins =>
        _origins ??= _clientsConfiguration.Proxy.Origins
            .Concat(_clientsConfiguration.Resources.Origins)
            .Concat(_clientsConfiguration.Web.Origins)
            .Select(u => u.OriginalString)
            .ToArray();

    public IdentityServerInMemoryConfiguration(
        ClientsConfiguration clientsConfiguration,
        bool isDevelopment)
    {
        _clientsConfiguration = clientsConfiguration;
        _isDevelopment = isDevelopment;
    }
}