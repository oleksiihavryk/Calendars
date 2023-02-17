using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendars.Authentication.Data.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Add data layer to application.
    ///     Adds services which needed for maintaining database in DI Container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns>
    ///     IServiceCollection instance after completing the operation.
    /// </returns>
    public static IServiceCollection AddDataLayer(
        this IServiceCollection services,
        string connectionString)
        => services.AddDbContext<AuthenticationIdentityDbContext>(optionsAction:
                opt => opt.UseSqlServer(connectionString));
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
}