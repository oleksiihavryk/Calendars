using System.Net;
using Calendars.Resources.Core.Interfaces;

namespace Calendars.Resources.Core;

/// <summary>
///     Response factory implementation.
///     Existed for creating response objects in system.
/// </summary>
public class ResponseFactory : IResponseFactory
{
    public Response CreateResponse(
        bool isSuccess,
        HttpStatusCode statusCode,
        object? result,
        params string[] messages)
        => new Response()
        {
            IsSuccess = isSuccess,
            Messages = messages,
            Result = result,
            StatusCode = (int)statusCode
        };
}