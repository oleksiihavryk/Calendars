using Calendars.Proxy.ActionResults;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.Controllers;
/// <summary>
///     Controller base what supported similar responses as a http response messages.
/// </summary>
public class SimilarResponseSupportedControllerBase : ControllerBase
{
    [NonAction]
    public IActionResult SimilarResponse(HttpResponseMessage responseMessage)
        => new SimilarResponseActionResult(responseMessage);
}