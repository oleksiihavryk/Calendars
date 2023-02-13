using Calendars.Resources.Domain;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Resources.Data.Extensions;
/// <summary>
///     Local DbSet`s extensions.
/// </summary>
internal static class DbSetExtensions
{
    internal static IQueryable<Calendar> GetFullCalendars(this DbSet<Calendar> dbSet) 
        => dbSet
            .Include(navigationPropertyPath: c => c.Days)
            .ThenInclude(navigationPropertyPath: d => d.Events);
    internal static IQueryable<Day> GetFullDays(this DbSet<Day> dbSet)
        => dbSet.Include(d => d.Events);
}