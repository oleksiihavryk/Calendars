using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.Controllers;

/// <summary>
///     Controller of event entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class EventController : SimilarResponseSupportedControllerBase
{
    private readonly IEventsService _eventsServices;

    public EventController(IEventsService eventsServices)
    {
        _eventsServices = eventsServices;
    }

    [HttpGet("id/{id:required}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] string id = "",
        [FromQuery] string userId = "")
    {
        var response = await _eventsServices.GetByIdAsync(id, userId);
        return SimilarResponse(response);
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromBody] Event @event)
    {
        var response = await _eventsServices.SaveAsync(@event);
        return SimilarResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Event @event)
    {
        var response = await _eventsServices.UpdateAsync(@event);
        return SimilarResponse(response);
    }
    [HttpDelete("id/{id:required}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] string id = "",
        [FromQuery] string userId = "")
    {
        var response = await _eventsServices.DeleteAsync(id, userId);
        return SimilarResponse(response);
    }
}