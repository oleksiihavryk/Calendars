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

    public Task<Event> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<Event> Save(Event @event)
    {
        throw new NotImplementedException();
    }
    public Task<Event> Update(Event @event)
    {
        throw new NotImplementedException();
    }
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}