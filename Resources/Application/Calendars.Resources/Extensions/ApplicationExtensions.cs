﻿using Calendars.Resources.Core.Extensions;
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
    ///     Use exception handler middleware.
    /// </summary>
    /// <param name="app"></param>
    /// <returns>
    ///     IApplicationBuilder implementation after completing the operation.
    /// </returns>
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlerMiddleware>();
}