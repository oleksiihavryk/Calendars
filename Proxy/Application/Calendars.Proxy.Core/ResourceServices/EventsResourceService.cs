using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.ResourceServices;
/// <summary>
///     Service for requesting events resources from resource server.
/// </summary>
public class EventsResourceService : IEventsResourceService
{
    private readonly IResourcesService _resourcesService;

    public EventsResourceService(IResourcesService resourcesService)
    {
        _resourcesService = resourcesService;
    }

    public virtual Task<HttpResponseMessage> GetByIdAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/event/id/{id}");
    public virtual Task<HttpResponseMessage> SaveAsync(Event @event)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/event",
            body: @event);
    public virtual Task<HttpResponseMessage> UpdateAsync(Event @event)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/event",
            body: @event);
    public virtual Task<HttpResponseMessage> DeleteAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/event/id/{id}");
}