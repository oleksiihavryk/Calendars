using AutoMapper;
using Calendars.Resources.Core.Interfaces;
using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.Controllers;

/// <summary>
///     Controller of event entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class EventController : ResponseSupportedControllerBase
{
    private readonly IMapper _autoMapper;
    private readonly IEventRepository _eventRepository;

    public EventController(
        IEventRepository eventRepository,
        IMapper autoMapper,
        IResponseFactory responseFactory)
        : base(responseFactory)
    {
        _eventRepository = eventRepository;
        _autoMapper = autoMapper;
    }

    [HttpGet("id/{id:required}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var day = await _eventRepository.GetByIdAsync(id);
            var result = _autoMapper.Map<EventDto>(day);

            return EntityFound(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromForm, FromBody] EventDto calendarDto)
    {
        var calendar = _autoMapper.Map<Event>(calendarDto);
        await _eventRepository.SaveAsync(calendar);
        var result = _autoMapper.Map<EventDto>(calendar);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm, FromBody] EventDto dayDto)
    {
        try
        {
            var day = _autoMapper.Map<Event>(dayDto);
            await _eventRepository.UpdateAsync(day);
            var result = _autoMapper.Map<EventDto>(day);

            return EntityUpdated(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(dayDto.Id);
        }
    }
    [HttpDelete("id/{id:required}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _eventRepository.DeleteAsync(id);
            return EntityDeleted();
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
}