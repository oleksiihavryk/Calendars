using Calendars.Proxy.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.Controllers;
/// <summary>
///     Controller of day entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class DayController
{
    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromForm, FromBody] Day day)
    {
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm, FromBody] Day day)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}