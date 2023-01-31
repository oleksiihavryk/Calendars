using Calendars.Resources.Data;

namespace Calendars.Resources.Extensions;
/// <summary>
///     Extensions of IConfiguration interface.
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    ///     Get connection string of single in system database.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>
    ///     Returns connection string.
    /// </returns>
    /// <exception cref="ApplicationException">
    ///     Occurred when application cannot get access to
    ///     connection string by default connection string key.
    /// </exception>
    internal static string GetSystemConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString(name: DatabaseDefaults.ConnectionStringKey)
           ?? throw new ApplicationException(
               message: "Application cannot get access to system connection string.");
}