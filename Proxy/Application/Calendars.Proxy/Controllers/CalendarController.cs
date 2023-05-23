using Calendars.Proxy.Core.Interfaces;
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
public class CalendarController : SimilarResponseSupportedControllerBase
{
    private readonly ICalendarsResourcesService _calendarsService;

    public CalendarController(ICalendarsResourcesService calendarsService)
    {
        _calendarsService = calendarsService;
    }

    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var response = await _calendarsService.GetByIdAsync(id);
        return SimilarResponse(response);
    }
    [HttpGet("user-id/{userId:required}")] 
    public async Task<IActionResult> GetAllByUserIdAsync([FromRoute] string userId)
    {
        var response = await _calendarsService.GetAllByUserIdAsync(userId);
        return SimilarResponse(response);
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromForm, FromBody] Calendar calendar)
    {
        var response = await _calendarsService.SaveAsync(calendar);
        return SimilarResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm, FromBody] Calendar calendar)
    {
        var response = await _calendarsService.UpdateAsync(calendar);
        return SimilarResponse(response);
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var response = await _calendarsService.DeleteAsync(id);
        return SimilarResponse(response);
    }
}