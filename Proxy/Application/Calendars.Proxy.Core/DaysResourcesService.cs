using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Calendars.Proxy.Domain;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core;

/// <summary>
///     Service for requesting days resources from resource server.
/// </summary>
public class DaysResourcesService : AuthenticationResourcesService, IDaysResourcesService
{
    public DaysResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<ResourcesServerOptions> resOpts,
        IOptions<AuthenticationServerOptions> authOpts)
        : base(clientFactory, resOpts, authOpts)
    {
    }

    public Task<Day> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<Day> Save(Day day)
    {
        throw new NotImplementedException();
    }
    public Task<Day> Update(Day day)
    {
        throw new NotImplementedException();
    }
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}