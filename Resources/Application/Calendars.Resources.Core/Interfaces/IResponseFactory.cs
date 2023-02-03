using System.Net;

namespace Calendars.Resources.Core.Interfaces;
/// <summary>
///     Response factory service interface.
/// </summary>
public interface IResponseFactory
{
    Response CreateResponse(
        bool isSuccess,
        HttpStatusCode statusCode,
        object? result,
        params string[] messages);

}