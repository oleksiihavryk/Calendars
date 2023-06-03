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

    public Task<HttpResponseMessage> GetByIdAsync(string id)
        => this.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar/id/{id}");
    public Task<HttpResponseMessage> SaveAsync(Day day)
        => this.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/calendar",
            body: day);
    public Task<HttpResponseMessage> UpdateAsync(Day day)
        => this.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/calendar",
            body: day);
    public Task<HttpResponseMessage> DeleteAsync(string id)
        => this.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/calendar/id/{id}");
}