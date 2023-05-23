using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;

/// <summary>
///     Service for access to days resources.
/// </summary>
public interface IDaysResourcesService
{
    public Task<Day> GetByIdAsync(Guid id);
    public Task<Day> Save(Day day);
    public Task<Day> Update(Day day);
    public Task Delete(Guid id);
}