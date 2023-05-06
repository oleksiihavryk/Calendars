using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Calendars.Resources.ActionResults;

/// <summary>
///     Derived from OkResult class which supported response objects.
/// </summary>
public class ResponseNotFoundResult : NotFoundObjectResult
{
    public const bool IsSuccess = true;
    public new const HttpStatusCode StatusCode = HttpStatusCode.NotFound;

    public ResponseNotFoundResult(
        IResponseFactory responseFactory,
        object? value,
        params string[] messages) 
        : base(responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: value,
            messages))
    {
    }
}