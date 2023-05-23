using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Calendars.Proxy.Domain;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core;
/// <summary>
///     Service for requesting calendars resources from resource server.
/// </summary>
public class CalendarsResourcesService : AuthenticationResourcesService, ICalendarsResourcesService
{
    public CalendarsResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<ResourcesServerOptions> resOpts,
        IOptions<AuthenticationServerOptions> authOpts) 
        : base(clientFactory, resOpts, authOpts)
    {
    }

    public virtual Task<IEnumerable<Calendar>> GetAllByUserId(string userId)
    {
        throw new NotImplementedException();
    }
    public Task<Calendar> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<Calendar> Save(Calendar calendar)
    {
        throw new NotImplementedException();
    }
    public Task<Calendar> Update(Calendar calendar)
    {
        throw new NotImplementedException();
    }
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}