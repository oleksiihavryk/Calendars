using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;

/// <summary>
///     Service for access to days resources.
/// </summary>
public interface IDaysService
{
    public Task<HttpResponseMessage> GetByIdAsync(string id, string userId);
    public Task<HttpResponseMessage> SaveAsync(Day day);
    public Task<HttpResponseMessage> UpdateAsync(Day day);
    public Task<HttpResponseMessage> DeleteAsync(string id, string userId);
}