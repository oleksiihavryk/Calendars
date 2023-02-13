using Calendars.Resources.Domain;

namespace Calendars.Resources.Data.Interfaces;

/// <summary>
///     Calendar repository interface.
/// </summary>
public interface ICalendarRepository : IRepositoryBase<Calendar, Guid>
{
    Task<IEnumerable<Calendar>> GetByUserIdAsync(string userId);
}