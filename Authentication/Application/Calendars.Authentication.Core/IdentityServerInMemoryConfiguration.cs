using Calendars.Authentication.Shared;
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

    public virtual List<Client> Clients => new()
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

            RedirectUris = _clientsConfiguration.Resources.Origins
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
        new Client()
        {
            ClientId = _clientsConfiguration.Authentication.Id,
            ClientName = _clientsConfiguration.Authentication.Name,
            ClientSecrets = new[]
            {
                new Secret { Value = _clientsConfiguration.Authentication.Secret.ToSha256() }
            },

            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = _clientsConfiguration.Authentication.Scopes,

            RedirectUris = _clientsConfiguration.Authentication.Origins
                .Select(o => new []
                {
                    o + "oauth2-redirect.html",
                    o + "signin-oidc"
                })
                .SelectMany(o => o)
                .ToArray(),
            PostLogoutRedirectUris = _clientsConfiguration.Authentication.Origins
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
    public virtual List<ApiScope> Scopes => new()
    {
        new ApiScope(ApplicationIdentityServerConstants.ResourcesApiScopeName),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };
    public virtual List<IdentityResource> Resources => new()
    {
        new IdentityResources.OpenId(),
        new IdentityResource()
        {
            Name = IdentityServerConstants.StandardScopes.Email,
            Description = ApplicationIdentityServerConstants.EmailIdentityResourceDescription,
            DisplayName = ApplicationIdentityServerConstants.EmailIdentityResourceName,
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
            DisplayName = ApplicationIdentityServerConstants.ProfileIdentityResourceName,
            Description = 
                ApplicationIdentityServerConstants.ProfileEmailIdentityResourceDescription
        }
    };
    public virtual List<ApiResource> ApiResources => new()
    {
        new ApiResource(ApplicationIdentityServerConstants.ResourcesApiResourcesName)
        {
            Scopes = { ApplicationIdentityServerConstants.ResourcesApiScopeName }
        },
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };
    public virtual string[] ClientsOrigins =>
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