using Calendars.Authentication.Core;
using Calendars.Authentication.Data;
using Calendars.Authentication.Domain;
using Microsoft.AspNetCore.Identity;

namespace Calendars.Authentication.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Add and configure IdentityServer and Identity features to application.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection instance after completing the operation.
    /// </returns>
    public static IServiceCollection AddConfiguredIdentityServer(this IServiceCollection services)
    {
        services.AddIdentity<User, UserRole>(opt =>
        {
            opt.Password = new PasswordOptions
            {
                RequiredLength = 8,
                RequiredUniqueChars = 3,
                RequireNonAlphanumeric = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            opt.User = new UserOptions
            {
                RequireUniqueEmail = true
            };
        }).AddEntityFrameworkStores<AuthenticationIdentityDbContext>();
        services.AddIdentityServer()
            .AddInMemoryClients(IdentityServerInMemoryConfiguration.Clients)
            .AddInMemoryApiScopes(IdentityServerInMemoryConfiguration.Scopes)
            .AddInMemoryIdentityResources(IdentityServerInMemoryConfiguration.IdentityResources)
            .AddInMemoryApiResources(IdentityServerInMemoryConfiguration.ApiResources)
            .AddDeveloperSigningCredential();

        return services;
    }
}