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

    public Task<HttpResponseMessage> GetByIdAsync(Guid id)
        => this.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar/id/{id}");
    public Task<HttpResponseMessage> Save(Event @event)
        => this.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/calendar",
            body: @event);
    public Task<HttpResponseMessage> UpdateAsync(Event @event)
        => this.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar",
            body: @event);
    public Task<HttpResponseMessage> DeleteAsync(Guid id)
        => this.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/calendar/id/{id}");
}