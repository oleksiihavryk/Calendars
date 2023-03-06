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

    public List<Client> Clients => new()
    {
        //Resources API client.
        new Client()
        {
            ClientId = _clientsConfiguration.Resources.Id,
            ClientName = _clientsConfiguration.Resources.Name,

            ClientSecrets = { new Secret(_clientsConfiguration.Resources.Secret.ToSha256()) },

            AllowedGrantTypes = GrantTypes.ClientCredentials,

            AllowedScopes = _clientsConfiguration.Resources.Scopes,

            RedirectUris =
            {
                _clientsConfiguration.Resources.Origin + "/oauth2-redirect.html",
                _clientsConfiguration.Resources.Origin + "/signin-oidc",
            },
            PostLogoutRedirectUris =
            {
                _clientsConfiguration.Resources.Origin + "/signout-callback-oidc"
            },
        },
        //Identity application client.
        new Client
        {
            ClientId = "web_cceb34b9-c5e9-4dcf-9830-70b830717389",
            
            ClientName = "web",
            ClientSecrets =
            {
                new Secret("cceb34b9-c5e9-4dcf-9830-70b830717389".ToSha256())
            },

            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Profile
            },

            RedirectUris =
            {
                _clientsConfiguration.Web.Origin + "/oauth2-redirect.html",
                _clientsConfiguration.Web.Origin + "/signin-oidc",
            },
            PostLogoutRedirectUris =
            {
                _clientsConfiguration.Web.Origin + "/signout-callback-oidc"
            },
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
            Required = true,
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

    public IdentityServerInMemoryConfiguration(ClientsConfiguration clientsConfiguration)
    {
        _clientsConfiguration = clientsConfiguration;
    }
}