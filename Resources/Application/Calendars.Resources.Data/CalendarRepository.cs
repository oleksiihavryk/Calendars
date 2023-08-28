using Calendars.Resources.Data.Extensions;
using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
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

    public async Task<Calendar> GetByIdAsync(Guid id, bool attached)
    {
        var entity = await _dbContext
            .Calendars
            .GetFullCalendars()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null)
            throw new ArgumentException(
                paramName: nameof(id),
                message: "Entity for update with passed entity id is not found in database.");

        if (attached == false)
            _dbContext.Calendars.Entry(entity).State = EntityState.Detached;

        return entity;
    }
    public async Task<IEnumerable<Calendar>> GetByUserIdAsync(string userId)
        => await _dbContext.Calendars
            .GetFullCalendars()
            .Where(c => c.UserId == userId)
            .ToArrayAsync();
    public async Task<Calendar> SaveAsync(Calendar entity)
    {
        _dbContext.Calendars.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<Calendar> UpdateAsync(Calendar entity)
    {
        var entry = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);

        _dbContext.Calendars.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}