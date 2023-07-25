﻿using Calendars.Proxy.Core.Interfaces;
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
    private readonly IDaysResourceService _daysServices;

    public DayController(IDaysResourceService daysServices)
    {
        _daysServices = daysServices;
    }

    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id = "")
    {
        var response = await _daysServices.GetByIdAsync(id);
        return SimilarResponse(response);
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromBody] Day day)
    {
        var response = await _daysServices.SaveAsync(day);
        return SimilarResponse(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Day day)
    {
        var response = await _daysServices.UpdateAsync(day);
        return SimilarResponse(response);
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id = "")
    {
        var response = await _daysServices.DeleteAsync(id);
        return SimilarResponse(response);
    }
}