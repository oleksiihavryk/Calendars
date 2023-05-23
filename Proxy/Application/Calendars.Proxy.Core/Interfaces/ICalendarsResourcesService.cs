using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Service for access to calendars resources.
/// </summary>
public interface ICalendarsResourcesService
{
    public Task<IEnumerable<Calendar>> GetAllByUserId(string userId);
    public Task<Calendar> GetByIdAsync(Guid id);
    public Task<Calendar> Save(Calendar calendar);
    public Task<Calendar> Update(Calendar calendar);
    public Task Delete(Guid id);
}