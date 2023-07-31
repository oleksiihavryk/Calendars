using System.Net;
using Calendars.Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Authentication.ActionResults;
/// <summary>
///     Derived from BadRequestResult class which supported response objects.
/// </summary>
public class ResponseBadRequestResult : IActionResult
{
    public const bool IsSuccess = false;
    public const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

    private readonly IResponseFactory _responseFactory;
    private readonly Dictionary<string, IEnumerable<string>> _parameterAndErrors;

    public ResponseBadRequestResult(
        IResponseFactory responseFactory,
        Dictionary<string, IEnumerable<string>> parameterAndErrors)
    {
        _responseFactory = responseFactory;
        _parameterAndErrors = parameterAndErrors;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var messages = _parameterAndErrors.Select(
            kvp => $"Errors of '{kvp.Key}' parameter: " +
                   kvp.Value.Aggregate((result, current) => result + ";" + current));
        var response = _responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: null,
            messages.ToArray());

        context.HttpContext.Response.StatusCode = response.StatusCode;
        await context.HttpContext.Response.WriteAsJsonAsync(response);
    }
}