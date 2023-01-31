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
        => services.AddScoped<IExceptionHandler, DefaultExceptionHandler>();
}