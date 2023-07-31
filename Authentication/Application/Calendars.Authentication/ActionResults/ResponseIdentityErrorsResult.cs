using Calendars.Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Calendars.Authentication.ActionResults;
/// <summary>
///     Implementation of IActionResult interface which supported response objects.
///     Is returns if operation with some identity resource is failed and
///     returns some identity errors.
/// </summary>
public class ResponseIdentityErrorsResult : IActionResult
{
    private readonly IResponseFactory _responseFactory;
    private readonly IEnumerable<IdentityError> _errors;

    public const bool IsSuccess = true;
    public const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

    public ResponseIdentityErrorsResult(
        IResponseFactory responseFactory,
        IEnumerable<IdentityError> errors)
    {
        _responseFactory = responseFactory;
        _errors = errors;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = _responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: null,
            messages: _errors.Select(m => m.Description).ToArray());

        context.HttpContext.Response.StatusCode = response.StatusCode;
        await context.HttpContext.Response.WriteAsJsonAsync(response);
    }
}