using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Services;
/// <summary>
///     Service for requesting days endpoints on resource server.
/// </summary>
public class DaysService : ServiceDecorator, IDaysService
{
    public DaysService(IService service) 
        : base(service) { }

    public virtual async Task<HttpResponseMessage> GetByIdAsync(string id, string userId)
        => await RequestAsync(
            method: HttpMethod.Get,
            path: $"day/id/{id}?userId={userId}");
    public virtual async Task<HttpResponseMessage> SaveAsync(Day day)
        => await RequestAsync(
            method: HttpMethod.Post,
            path: $"day",
            body: day);
    public virtual async Task<HttpResponseMessage> UpdateAsync(Day day)
        => await RequestAsync(
            method: HttpMethod.Put,
            path: $"day",
            body: day);
    public virtual async Task<HttpResponseMessage> DeleteAsync(string id, string userId)
        => await RequestAsync(
            method: HttpMethod.Delete,
            path: $"day/id/{id}?userId={userId}");
}