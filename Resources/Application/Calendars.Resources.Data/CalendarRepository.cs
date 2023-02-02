﻿using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Resources.Data;

/// <summary>
///     Repository class of calendar entity.
///     Provide easy access to database.
/// </summary>
public class CalendarRepository : ICalendarRepository
{
    private readonly CalendarsDbContext _dbContext;

    public CalendarRepository(CalendarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Calendar> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext
            .Calendars
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null)
            throw new ArgumentException(
                paramName: nameof(id),
                message: "Entity for update with passed entity id is not found in database.");

        return entity;
    }
    public async Task<IEnumerable<Calendar>> GetByUserIdAsync(string userId)
    {
        var entities = (await _dbContext
            .Calendars
            .ToArrayAsync())
            .Where(c => c.UserId == userId);

        return entities;
    }
    public async Task<Calendar> SaveAsync(Calendar entity)
    {
        _dbContext.Calendars.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<Calendar> UpdateAsync(Calendar entity)
    {
        var updateEntity = await GetByIdAsync(entity.Id);

        updateEntity.ShallowUpdateProperties(entity, nameof(Calendar.Id));

        return updateEntity;
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        _dbContext.Calendars.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}