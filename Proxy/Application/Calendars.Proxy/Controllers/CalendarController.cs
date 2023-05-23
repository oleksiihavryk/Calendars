using Calendars.Proxy.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.Controllers;
/// <summary>
///     Controller of calendar entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class CalendarController
{
    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
    [HttpGet("user-id/{userId:required}")] 
    public async Task<IActionResult> GetAllByUserId([FromRoute] string userId)
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromForm, FromBody] Calendar calendar)
    {
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm, FromBody] Calendar calendar)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}