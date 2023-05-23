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

    public Task<HttpResponseMessage> GetAllByUserIdAsync(string userId)
        => this.RequestResourceAsync(
            method: HttpMethod.Get, 
            path: $"/calendar/user-id/{userId}");
    public Task<HttpResponseMessage> GetByIdAsync(Guid id)
        => this.RequestResourceAsync(
            method: HttpMethod.Get,
            path: $"/calendar/id/{id}");
    public Task<HttpResponseMessage> SaveAsync(Calendar calendar)
        => this.RequestResourceAsync(
            method: HttpMethod.Post,
            path: $"/calendar",
            body: calendar);
    public Task<HttpResponseMessage> UpdateAsync(Calendar calendar)
        => this.RequestResourceAsync(
            method: HttpMethod.Put,
            path: $"/calendar",
            body: calendar);
    public Task<HttpResponseMessage> DeleteAsync(Guid id)
        => this.RequestResourceAsync(
            method: HttpMethod.Delete,
            path: $"/calendar/id/{id}");
}