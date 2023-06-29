using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Calendars.Proxy.Domain;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core;
/// <summary>
///     Service for requesting events resources from resource server.
/// </summary>
public class EventsResourcesService : AuthenticationResourcesService, IEventsResourcesService
{
    public EventsResourcesService(
        IHttpClientFactory clientFactory, 
        IOptions<ResourcesServerOptions> resOpts, 
        IOptions<AuthenticationServerOptions> authOpts) 
        : base(clientFactory, resOpts, authOpts)
    {
    }

    public Task<HttpResponseMessage> GetByIdAsync(string id)
        => this.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/event/id/{id}");
    public Task<HttpResponseMessage> Save(Event @event)
        => this.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/event",
            body: @event);
    public Task<HttpResponseMessage> UpdateAsync(Event @event)
        => this.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/event",
            body: @event);
    public Task<HttpResponseMessage> DeleteAsync(string id)
        => this.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/event/id/{id}");
}