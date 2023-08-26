using AutoMapper;
using Calendars.Resources.Core.Interfaces;
using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.Controllers;
/// <summary>
///     Controller of day entity.
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class DayController : ResponseSupportedControllerBase
{
    private readonly IMapper _autoMapper;
    private readonly IDayRepository _dayRepository;

    public DayController(
        IDayRepository dayRepository,
        IMapper autoMapper,
        IResponseFactory responseFactory)
        : base(responseFactory)
    {
        _dayRepository = dayRepository;
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
            var day = await _dayRepository.GetByIdAsync(id, false);
            var result = _autoMapper.Map<DayDto>(day);

            return EntityFound(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(id);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] DayDto dayDto)
    {
        if (dayDto.Id != Guid.Empty && 
            await CheckIfEntityHaveCorrectUserIdAsync(dayDto.Id, dayDto.UserId) == false)
            return UnknownUserIdentifier(dayDto.UserId);

        var day = _autoMapper.Map<Day>(dayDto);
        await _dayRepository.SaveAsync(day);
        var result = _autoMapper.Map<DayDto>(day);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DayDto dayDto)
    {
        if (dayDto.Id != Guid.Empty &&
            await CheckIfEntityHaveCorrectUserIdAsync(dayDto.Id, dayDto.UserId) == false)
            return UnknownUserIdentifier(dayDto.UserId);

        try
        {
            var day = _autoMapper.Map<Day>(dayDto);
            await _dayRepository.UpdateAsync(day);
            var result = _autoMapper.Map<DayDto>(day);

            return EntityUpdated(result);
        }
        catch (ArgumentException)
        {
            return UnknownIdentifier(dayDto.Id);
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
            await _dayRepository.DeleteAsync(id);
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
            var entity = await _dayRepository.GetByIdAsync(id, false);
            return entity.UserId.Equals(userId, StringComparison.Ordinal);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}