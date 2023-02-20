using Calendars.Resources.Core.Extensions;
using Calendars.Resources.Domain;
using Calendars.Resources.Dto;
using Calendars.Resources.Filters;
using Calendars.Resources.Middleware;

namespace Calendars.Resources.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
internal static class ApplicationExtensions
{
    /// <summary>
    ///     Add exception handler service to pipeline.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    internal static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
    {
        //Add exception handler service.
        services.AddExceptionHandlerCore();

        //Add exception handler middleware.
        services.AddScoped<ExceptionHandlerMiddleware>();

        return services;
    }
    /// <summary>
    ///     Add auto mapper service to DI Container with pre-configured settings.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    internal static IServiceCollection AddAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(mapper =>
        {
            mapper.CreateDoubleLinkedMap<Calendar, CalendarDto>();
            mapper.CreateDoubleLinkedMap<Day, DayDto>();
            mapper.CreateDoubleLinkedMap<Event, EventDto>();
        });
    /// <summary>
    ///     Use exception handler middleware.
    /// </summary>
    /// <param name="app"></param>
    /// <returns>
    ///     IApplicationBuilder implementation after completing the operation.
    /// </returns>
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlerMiddleware>();
    /// <summary>
    ///     Add global model state validating filter into DI container.
    ///     Additionally need to register this filter in application.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection instance after completing the operation.
    /// </returns>
    internal static IServiceCollection AddModelStateValidatingGlobalFilter(
        this IServiceCollection services)
        => services.AddScoped<CustomModelStateActionFilter>();
    
}