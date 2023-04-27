using Microsoft.EntityFrameworkCore;
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
            opt =>
            {
                opt.UseSqlServer(connectionString);
            });
}