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
            var @event = await _eventRepository.GetByIdAsync(id);
            var result = _autoMapper.Map<EventDto>(@event);

            return EntityFound(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] EventDto eventDto)
    {
        var @event = _autoMapper.Map<Event>(eventDto);
        await _eventRepository.SaveAsync(@event);
        var result = _autoMapper.Map<EventDto>(@event);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EventDto eventDto)
    {
        try
        {
            var @event = _autoMapper.Map<Event>(eventDto);
            await _eventRepository.UpdateAsync(@event);
            var result = _autoMapper.Map<EventDto>(@event);

            return EntityUpdated(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(eventDto.Id);
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