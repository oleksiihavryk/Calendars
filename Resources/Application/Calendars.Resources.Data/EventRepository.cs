using Calendars.Resources.Data.Interfaces;
using Calendars.Resources.Domain;
using Calendars.Resources.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Resources.Data;

/// <summary>
///     Repository class of event entity.
///     Provide easy access to database.
/// </summary>
public class EventRepository : IEventRepository
{
    private readonly CalendarsDbContext _dbContext;

    public EventRepository(CalendarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext
            .Events
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null)
            throw new ArgumentException(
                paramName: nameof(id),
                message: "Entity for update with passed entity id is not found in database.");

        return entity;
    }
    public async Task<Event> SaveAsync(Event entity)
    {
        _dbContext.Events.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<Event> UpdateAsync(Event entity)
    {
        var updateEntity = await GetByIdAsync(entity.Id);

        updateEntity.ShallowUpdateProperties(entity, nameof(Event.Id));

        return updateEntity;
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        _dbContext.Events.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}