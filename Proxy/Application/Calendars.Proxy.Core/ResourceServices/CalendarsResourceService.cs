using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.ResourceServices;
/// <summary>
///     Service for requesting calendars resources from resource server.
/// </summary>
public class CalendarsResourceService : ICalendarsResourceService
{
    private readonly IResourcesService _resourcesService;

    public CalendarsResourceService(IResourcesService resourcesService)
    {
        _resourcesService = resourcesService;
    }

    public virtual Task<HttpResponseMessage> GetAllByUserIdAsync(string userId)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar/user-id/{userId}");
    public virtual Task<HttpResponseMessage> GetByIdAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar/id/{id}");
    public virtual Task<HttpResponseMessage> SaveAsync(Calendar calendar)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/calendar",
            body: calendar);
    public virtual Task<HttpResponseMessage> UpdateAsync(Calendar calendar)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/calendar",
            body: calendar);
    public virtual Task<HttpResponseMessage> DeleteAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/calendar/id/{id}");
}