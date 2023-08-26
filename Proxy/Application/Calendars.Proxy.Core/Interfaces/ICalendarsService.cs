using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Service for access to calendars resources.
/// </summary>
public interface ICalendarsService
{
    public Task<HttpResponseMessage> GetAllByUserIdAsync(string userId);
    public Task<HttpResponseMessage> GetByIdAsync(string id, string userId);
    public Task<HttpResponseMessage> SaveAsync(Calendar calendar);
    public Task<HttpResponseMessage> UpdateAsync(Calendar calendar);
    public Task<HttpResponseMessage> DeleteAsync(string id, string userId);
}