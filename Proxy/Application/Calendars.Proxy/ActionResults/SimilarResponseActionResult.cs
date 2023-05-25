using Calendars.Proxy.Core;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.ActionResults;
/// <summary>
///     Similar response as a http response message action result.
/// </summary>
public class SimilarResponseActionResult : IActionResult
{
    public const string ContentType = "application/json";

    private readonly HttpResponseMessage _responseMessage;

    public SimilarResponseActionResult(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = (int)_responseMessage.StatusCode;
        response.ContentType = ContentType;

        var responseBody = await _responseMessage.Content.ReadFromJsonAsync<Response>();
        await response.WriteAsJsonAsync(responseBody);
    }
}