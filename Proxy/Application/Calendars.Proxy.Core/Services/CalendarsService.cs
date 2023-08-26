using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Services;
/// <summary>
///     Service for requesting calendars endpoints on resource server.
/// </summary>
public class CalendarsService : ServiceDecorator, ICalendarsService
{
    public CalendarsService(IService service) 
        : base(service) { }

    public virtual async Task<HttpResponseMessage> GetAllByUserIdAsync(string userId)
        => await RequestAsync(
            method: HttpMethod.Get,
            path: $"calendar/user-id/{userId}");
    public virtual async Task<HttpResponseMessage> GetByIdAsync(string id, string userId)
        => await RequestAsync(
            method: HttpMethod.Get,
            path: $"calendar/id/{id}?userId={userId}");
    public virtual async Task<HttpResponseMessage> SaveAsync(Calendar calendar)
        => await RequestAsync(
            method: HttpMethod.Post,
            path: $"calendar",
            body: calendar);
    public virtual async Task<HttpResponseMessage> UpdateAsync(Calendar calendar)
        => await RequestAsync(
            method: HttpMethod.Put,
            path: $"calendar",
            body: calendar);
    public virtual async Task<HttpResponseMessage> DeleteAsync(string id, string userId)
        => await RequestAsync(
            method: HttpMethod.Delete,
            path: $"calendar/id/{id}?userId={userId}");
}