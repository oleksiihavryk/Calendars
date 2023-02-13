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
    {
        if (Enum.IsDefined(statusCode) == false)
            throw new ArgumentException(
                paramName: nameof(statusCode), 
                message: "Passed status code number is not defined.");

        return new Response()
        {
            IsSuccess = isSuccess,
            Messages = messages,
            Result = result,
            StatusCode = (int)statusCode
        };
    }
}