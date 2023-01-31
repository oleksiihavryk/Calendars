using Calendars.Resources.Core;

namespace Calendars.Resources.Middlewares;
/// <summary>
///     Exception handler middleware.
/// </summary>
public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly IExceptionHandler _handler;

    public ExceptionHandlerMiddleware(IExceptionHandler handler)
    {
        _handler = handler;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await _handler.HandleAsync(ex);
        }
    }
}