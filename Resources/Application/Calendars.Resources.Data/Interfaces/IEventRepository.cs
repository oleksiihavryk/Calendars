using Calendars.Resources.Domain;

namespace Calendars.Resources.Data.Interfaces;

/// <summary>
///     Event repository interface.
/// </summary>
public interface IEventRepository : IRepositoryBase<Event, Guid>
{
}