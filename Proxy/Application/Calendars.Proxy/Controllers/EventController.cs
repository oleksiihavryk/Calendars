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
    private readonly IEventsResourcesService _eventsServices;

    public EventController(IEventsResourcesService eventsServices)
    {
        _eventsServices = eventsServices;
    }

    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id = "")
    {
        var response = await _eventsServices.GetByIdAsync(id);
        return SimilarResponse(response);
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromForm, FromBody] Event @event)
    {
        var response = await _eventsServices.Save(@event);
        return SimilarResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm, FromBody] Event @event)
    {
        var response = await _eventsServices.UpdateAsync(@event);
        return SimilarResponse(response);
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id = "")
    {
        var response = await _eventsServices.DeleteAsync(id);
        return SimilarResponse(response);
    }
}