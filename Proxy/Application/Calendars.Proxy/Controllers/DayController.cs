using Calendars.Proxy.Core.Interfaces;
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
public class DayController : SimilarResponseSupportedControllerBase
{
    private readonly IDaysResourcesService _daysServices;

    public DayController(IDaysResourcesService daysServices)
    {
        _daysServices = daysServices;
    }

    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var response = await _daysServices.GetByIdAsync(id);
        return SimilarResponse(response);
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromForm, FromBody] Day day)
    {
        var response = await _daysServices.SaveAsync(day);
        return SimilarResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm, FromBody] Day day)
    {
        var response = await _daysServices.UpdateAsync(day);
        return SimilarResponse(response);
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var response = await _daysServices.DeleteAsync(id);
        return SimilarResponse(response);
    }
}