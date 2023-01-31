using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Calendars.Resources.Data.Extensions;
/// <summary>
///     Application extensions. 
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Add data layer to the system.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">
    ///     Connection string to database.
    /// </param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    public static IServiceCollection AddDataLayer(
        this IServiceCollection services,
        string connectionString)
        => services.AddDbContext<CalendarsDbContext>(
            optionsAction: options => options.UseSqlServer(
                connectionString));
}