using System.Net;
using Calendars.Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Authentication.ActionResults;
/// <summary>
///     Derived from OkResult class which supported response objects.
/// </summary>
public class ResponseOkResult : OkObjectResult
{
    public const bool IsSuccess = true;
    public const HttpStatusCode StatusCode = HttpStatusCode.OK;

    public ResponseOkResult(
        IResponseFactory responseFactory,
        object? value,
        params string[] messages)
        : base(responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: value,
            messages: messages))
    {
    }
}