using System.Net;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.ActionResults;
/// <summary>
///     Derived from OkResult class which supported response objects.
/// </summary>
public class ResponseOkObjectResult : OkObjectResult
{
    public const bool IsSuccess = true;
    public const HttpStatusCode StatusCode = HttpStatusCode.OK;

    public ResponseOkObjectResult(
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