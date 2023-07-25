using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.ResourceServices;

/// <summary>
///     Service for requesting days resources from resource server.
/// </summary>
public class DaysResourceService : IDaysResourceService
{
    private readonly IResourcesService _resourcesService;

    public DaysResourceService(IResourcesService resourcesService)
    {
        _resourcesService = resourcesService;
    }

    public virtual Task<HttpResponseMessage> GetByIdAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/day/id/{id}");
    public virtual Task<HttpResponseMessage> SaveAsync(Day day)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/day",
            body: day);
    public virtual Task<HttpResponseMessage> UpdateAsync(Day day)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/day",
            body: day);
    public virtual Task<HttpResponseMessage> DeleteAsync(string id)
        => _resourcesService.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/day/id/{id}");
}