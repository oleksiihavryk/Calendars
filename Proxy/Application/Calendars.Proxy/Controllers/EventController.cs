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
public class EventController
{
    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromForm, FromBody] Event @event)
    {
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm, FromBody] Event @event)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}