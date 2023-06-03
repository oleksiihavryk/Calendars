using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;

/// <summary>
///     Service for access to events resources.
/// </summary>
public interface IEventsResourcesService
{
    public Task<HttpResponseMessage> GetByIdAsync(string id);
    public Task<HttpResponseMessage> Save(Event @event);
    public Task<HttpResponseMessage> UpdateAsync(Event @event);
    public Task<HttpResponseMessage> DeleteAsync(string id);
}