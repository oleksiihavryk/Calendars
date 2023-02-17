using IdentityServer4.Models;

namespace Calendars.Authentication.Core;
/// <summary>
///     In memory configuration of IdentityServer4 framework.
///     All clients, scopes and resources of authentication server.
/// </summary>
public static class IdentityServerInMemoryConfiguration
{
    public static List<Client> Clients => new()
    {
        //Resources API client.
        new Client()
        {
            ClientId = 
        },
        //Identity application client.
        new Client()
        {

        }
    };
    public static List<ApiScope> Scopes => new()
    {
    };
    public static List<IdentityResource> IdentityResources => new()
    {
    };
    public static List<ApiResource> ApiResources => new()
    {
    };
}