﻿using AutoMapper;
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
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var day = await _dayRepository.GetByIdAsync(id);
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
        var day = _autoMapper.Map<Day>(dayDto);
        await _dayRepository.SaveAsync(day);
        var result = _autoMapper.Map<DayDto>(day);

        return EntityCreated(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DayDto dayDto)
    {
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
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
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
}