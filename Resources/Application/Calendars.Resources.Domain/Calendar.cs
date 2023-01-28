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

    [Required] [StringLength(128)] public string Name { get; set; } = UnnamedCalendarName;
    [Required] public int Year { get; set; }
    [Required] public CalendarType Type { get; set; }
    public IEnumerable<SpecialDay> SpecialDays { get; set; } = Enumerable.Empty<SpecialDay>();
}