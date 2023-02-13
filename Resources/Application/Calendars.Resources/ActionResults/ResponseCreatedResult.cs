using System.Net;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.ActionResults;
/// <summary>
///     Derived from CreatedResult class which supported response objects.
/// </summary>
public class ResponseCreatedResult : IActionResult
{
    public const bool IsSuccess = true;
    public const HttpStatusCode StatusCode = HttpStatusCode.Created;

    private readonly IResponseFactory _responseFactory;
    private readonly object? _value;
    private readonly string[] _messages;

    public ResponseCreatedResult(
        IResponseFactory responseFactory,
        object? value, 
        params string[] messages)
    {
        _responseFactory = responseFactory;
        _value = value;
        _messages = messages;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = _responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: _value,
            messages: _messages);

        context.HttpContext.Response.StatusCode = response.StatusCode;
        await context.HttpContext.Response.WriteAsJsonAsync(response);
    }
}