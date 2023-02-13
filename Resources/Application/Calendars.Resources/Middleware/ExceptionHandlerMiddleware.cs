using System.Net;
using Calendars.Resources.Core.Interfaces;

namespace Calendars.Resources.Middleware;
/// <summary>
///     Exception handler middleware.
/// </summary>
public class ExceptionHandlerMiddleware : IMiddleware
{
    private const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
    private const bool IsSuccess = false;

    private readonly IExceptionHandler _handler;
    private readonly IResponseFactory _responseFactory;

    public ExceptionHandlerMiddleware(
        IExceptionHandler handler, 
        IResponseFactory responseFactory)
    {
        _handler = handler;
        _responseFactory = responseFactory;
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
            await HandleResponseAsync(context);
        }
    }

    private async Task HandleResponseAsync(HttpContext ctx)
    {
        var response = _responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: null,
            messages: "Unknown and unexpected internal server error. " +
                      "Try to reach the developer to known a reason of an error.");

        ctx.Response.StatusCode = response.StatusCode;
        await ctx.Response.WriteAsJsonAsync(response);
    }
}