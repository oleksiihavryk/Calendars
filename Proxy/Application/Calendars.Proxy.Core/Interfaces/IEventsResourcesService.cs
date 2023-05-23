using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;

/// <summary>
///     Service for access to events resources.
/// </summary>
public interface IEventsResourcesService
{
    public Task<Event> GetByIdAsync(Guid id);
    public Task<Event> Save(Event @event);
    public Task<Event> Update(Event @event);
    public Task Delete(Guid id);
}