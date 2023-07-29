using Calendars.Authentication.Core;
using Calendars.Authentication.Data;
using Calendars.Authentication.Domain;
using IdentityServer4.Validation;
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
    public static IServiceCollection AddIdentityServer(
        this IServiceCollection services,
        IdentityServerInMemoryConfiguration identityServerInMemoryConfiguration)
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
                RequireUniqueEmail = false,
            };
        }).AddEntityFrameworkStores<AuthenticationIdentityDbContext>();
        services.AddIdentityServer()
            .AddInMemoryClients(identityServerInMemoryConfiguration.Clients)
            .AddInMemoryApiScopes(identityServerInMemoryConfiguration.Scopes)
            .AddInMemoryIdentityResources(identityServerInMemoryConfiguration.Resources)
            .AddInMemoryApiResources(identityServerInMemoryConfiguration.ApiResources)
            .AddDeveloperSigningCredential()
            .AddSecretValidator<PkceSecretValidator>();

        services.AddLocalApiAuthentication();

        services.AddSingleton(identityServerInMemoryConfiguration);

        return services;
    }
    /// <summary>
    ///     Shorthand for <code>GetConnectionString(name: "Authentication")</code>
    /// </summary>
    /// <param name="config"></param>
    /// <returns>
    ///     The connection string.
    /// </returns>
    /// <exception cref="ApplicationException"></exception>
    public static string GetAuthenticationConnectionString(this IConfiguration config)
        => config.GetConnectionString(name: "Authentication")
           ?? throw new ApplicationException(
               message: "Cannot get authentication database " +
                        "connection string from configuration file.");
    /// <summary>
    ///     Get one of web client urls or return null.
    /// </summary>
    /// <param name="config"></param>
    /// <returns>
    ///     String of url or null if url is not found.
    /// </returns>
    public static string? GetOneOfWebClientUrlsOrReturnNull(this IConfiguration config)
    {
        var urls = config
            .GetSection("Clients:Web:Origins")
            .Get<string[]>();

        if (UrlsIsNullOrEmpty(urls)) return null;

        return urls?.FirstOrDefault(s => s.Contains("https://")) ??
               urls?.FirstOrDefault(s => s.Contains("http://"));
    }
    /// <summary>
    ///     Seed all roles which contains enum "Roles" in system.
    /// </summary>
    /// <param name="app"></param>
    public static async Task SeedRolesAsync(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
        foreach (var r in Enum.GetValues<Roles>())
            if (await roleManager.RoleExistsAsync(r.ToString()) == false)
                await roleManager.CreateAsync(UserRole.CreateRole(r)); 
    }

    private static bool UrlsIsNullOrEmpty(string[]? urls)
        => urls == null || urls.Length == 0;
}