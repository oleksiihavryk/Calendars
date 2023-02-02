using System.ComponentModel.DataAnnotations;

namespace Calendars.Resources.Domain;
/// <summary>
///     Calendar - main operational unit in domain.
///     Model of calendar. 
/// </summary>
public class Calendar
{
    public const string UnnamedCalendarName = "Unnamed";

    [Key] public Guid Id { get; set; }
    [Required] public string UserId { get; set; }

    [Required] [StringLength(32)] public string Name { get; set; } = UnnamedCalendarName;
    [Required] public int Year { get; set; }
    [Required] public CalendarType Type { get; set; }
    public IEnumerable<Day> Days { get; set; } = Enumerable.Empty<Day>();
}