using Calendars.Resources.Domain;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Resources.Data;
/// <summary>
///     Database context of Calendars system.
/// </summary>
public class CalendarsDbContext : DbContext
{
    public DbSet<Calendar> Calendars { get; set; } = null!;
    public DbSet<Day> Days { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;

    public CalendarsDbContext(DbContextOptions<CalendarsDbContext> options)
        : base(options)
    {
    }
}