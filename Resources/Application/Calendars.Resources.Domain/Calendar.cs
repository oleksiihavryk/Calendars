namespace Calendars.Resources.Domain;
/// <summary>
///     Calendar - main operational unit in domain.
///     Model of calendar. 
/// </summary>
public class Calendar
{
    public const string UnnamedCalendarName = "Unnamed";

    public Guid Id { get; set; }
    public string UserId { get; set; }

    public string Name { get; set; } = UnnamedCalendarName;
    public int Year { get; set; }
    public CalendarType Type { get; set; }
    public ICollection<Day> Days { get; set; } = new List<Day>();
}