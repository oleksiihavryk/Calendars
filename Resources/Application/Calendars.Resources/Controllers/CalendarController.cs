using AutoMapper;
using Calendars.Resources.Core.Interfaces;
using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.Controllers;
/// <summary>
///     Controller of calendar entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class CalendarController : ResponseSupportedControllerBase
{
    private readonly IMapper _autoMapper;
    private readonly ICalendarRepository _calendarRepository;

    public CalendarController(
        ICalendarRepository calendarRepository, 
        IMapper autoMapper,
        IResponseFactory responseFactory) 
        : base(responseFactory)
    {
        _calendarRepository = calendarRepository;
        _autoMapper = autoMapper;
    }

    [HttpGet("id/{id:guid:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            var result = _autoMapper.Map<CalendarDto>(calendar);

            return EntityFound(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
    [HttpGet("user-id/{userId:required}")] 
    public async Task<IActionResult> GetAllByUserId([FromRoute] string userId)
    {
        var calendars = await _calendarRepository.GetByUserIdAsync(userId);
        var result = _autoMapper.Map<IEnumerable<CalendarDto>>(calendars);

        return result.Any() == false ? UnknownIdentifier(userId) : EntityFound(result);
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromForm, FromBody] CalendarDto calendarDto)
    {
        var calendar = _autoMapper.Map<Calendar>(calendarDto);
        await _calendarRepository.SaveAsync(calendar);
        var result = _autoMapper.Map<CalendarDto>(calendar);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm, FromBody] CalendarDto calendarDto)
    {
        try
        {
            var calendar = _autoMapper.Map<Calendar>(calendarDto);
            await _calendarRepository.UpdateAsync(calendar);
            var result = _autoMapper.Map<CalendarDto>(calendar);

            return EntityUpdated(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(calendarDto.Id);
        }
    }
    [HttpDelete("id/{id:guid:required}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _calendarRepository.DeleteAsync(id);
            return EntityDeleted();
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
}