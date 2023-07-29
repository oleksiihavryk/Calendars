using Calendars.Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Calendars.Authentication.ActionResults;
/// <summary>
///     Derived from ObjectResult class which supported response objects.
///     Is returns if operation with some identity resource is failed and
///     returns some identity errors.
/// </summary>
public class ResponseIdentityErrorsResult : ObjectResult
{
    public const bool IsSuccess = true;
    public const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

    public ResponseIdentityErrorsResult(
        IResponseFactory responseFactory,
        IEnumerable<IdentityError> errors) 
        : base(responseFactory.CreateResponse(
            isSuccess: IsSuccess,
            statusCode: StatusCode,
            result: null,
            messages: errors.Select(m => m.Description).ToArray()))
    {
    }
}