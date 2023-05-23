using System.Drawing;

namespace Calendars.Resources.Domain;

/// <summary>
///     Special day of calendar (like some holiday, birthday, anniversary or etc.)
/// </summary>
public class Day
{
    public const KnownColor DefaultColor = KnownColor.Red;

    public Guid Id { get; set; }
    public Guid CalendarId { get; set; }

    public int DayNumber { get; set; }
    public int ArgbColorInteger { get; set; } = Color.FromKnownColor(DefaultColor).ToArgb();
    public ICollection<Event> Events { get; set; } 
        = new List<Event>();
}