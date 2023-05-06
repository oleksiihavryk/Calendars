using Calendars.Resources.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendars.Resources.Core.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Add exception handler to DI Container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    public static IServiceCollection AddExceptionHandlerCore(this IServiceCollection services)
        => services.AddScoped<IExceptionHandler, ExceptionHandler>();
    /// <summary>
    ///     Add response factory in DI Container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    public static IServiceCollection AddResponseFactory(this IServiceCollection services)
        => services.AddSingleton<IResponseFactory, ResponseFactory>();
    /// <summary>
    ///     Create authentication configuration object and fill it up
    ///     with data from configuration file.
    /// </summary>
    /// <param name="config"></param>
    /// <returns>
    ///     AuthenticationConfiguration object.
    /// </returns>
    public static AuthenticationConfiguration AssembleAuthenticationConfiguration(
        this IConfiguration config)
        => new AuthenticationConfiguration(
            uri: config["Authentication:Uri"] ?? string.Empty,
            audience: config["Authentication:Audience"] ?? string.Empty,
            scope: config["Authentication:Scope"] ?? string.Empty);
}