using Calendars.Proxy.Domain;
using Calendars.Proxy.RefactoredCore.Interfaces;

namespace Calendars.Proxy.RefactoredCore.Services;
/// <summary>
///     Service for requesting events endpoints on resource server.
/// </summary>
public class EventsService : ServiceDecorator, IEventsService
{
    public EventsService(IService service) 
        : base(service) { }

    public virtual async Task<HttpResponseMessage> GetByIdAsync(string id)
        => await RequestAsync(
            method: HttpMethod.Get,
            path: $"/event/id/{id}");
    public virtual async Task<HttpResponseMessage> SaveAsync(Event @event)
        => await RequestAsync(
            method: HttpMethod.Post,
            path: $"/event",
            body: @event);
    public virtual async Task<HttpResponseMessage> UpdateAsync(Event @event)
        => await RequestAsync(
            method: HttpMethod.Put,
            path: $"/event",
            body: @event);
    public virtual async Task<HttpResponseMessage> DeleteAsync(string id)
        => await RequestAsync(
            method: HttpMethod.Delete,
            path: $"/event/id/{id}");
}