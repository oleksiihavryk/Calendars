using Calendars.Resources.Data.Extensions;
using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Resources.Data;

/// <summary>
///     Repository class of day entity.
///     Provide easy access to database.
/// </summary>
public class DayRepository : IDayRepository
{
    private readonly CalendarsDbContext _dbContext;

    public DayRepository(CalendarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Day> GetByIdAsync(Guid id, bool attached)
    {
        var entity = await _dbContext
            .Days
            .GetFullDays()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null)
            throw new ArgumentException(
                paramName: nameof(id),
                message: "Entity for update with passed entity id is not found in database.");

        if (attached == false)
            _dbContext.Days.Entry(entity).State = EntityState.Detached;

        return entity;
    }
    public async Task<Day> SaveAsync(Day entity)
    {
        _dbContext.Days.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<Day> UpdateAsync(Day entity)
    {
        var entry = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);

        _dbContext.Days.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}