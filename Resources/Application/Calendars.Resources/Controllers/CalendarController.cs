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

    [HttpGet("id/{id:required}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id, 
        [FromQuery] string userId)
    {
        if (await CheckIfEntityHaveCorrectUserIdAsync(id, userId) == false)
            return UnknownUserIdentifier(userId);

        try
        {
            var calendar = await _calendarRepository.GetByIdAsync(id, false);
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
    public async Task<IActionResult> Save([FromBody] CalendarDto calendarDto)
    {
        if (calendarDto.Id != Guid.Empty && 
            await CheckIfEntityHaveCorrectUserIdAsync(calendarDto.Id, calendarDto.UserId) == false)
            return UnknownUserIdentifier(calendarDto.UserId);

        var calendar = _autoMapper.Map<Calendar>(calendarDto);
        await _calendarRepository.SaveAsync(calendar);
        var result = _autoMapper.Map<CalendarDto>(calendar);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CalendarDto calendarDto)
    {
        if (calendarDto.Id != Guid.Empty && 
            await CheckIfEntityHaveCorrectUserIdAsync(calendarDto.Id, calendarDto.UserId) == false)
            return UnknownUserIdentifier(calendarDto.UserId);

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
    [HttpDelete("id/{id:required}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromQuery] string userId)
    {
        if (await CheckIfEntityHaveCorrectUserIdAsync(id, userId) == false)
            return UnknownUserIdentifier(userId);

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

    private async Task<bool> CheckIfEntityHaveCorrectUserIdAsync(Guid id, string userId)
    {
        try
        {
            var entity = await _calendarRepository.GetByIdAsync(id, false);
            return entity.UserId.Equals(userId, StringComparison.Ordinal);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}