using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Calendars.Resources.Domain;

/// <summary>
///     Special day of calendar (like some holiday, birthday, anniversary or etc.)
/// </summary>
public class Day
{
    [Required] public Guid Id { get; set; }
    [Required] public Guid CalendarId { get; set; }

    [Required] public int DayNumber { get; set; }
    [Required] public int BackgroundArgbColorInteger { get; set; }
    [Required] public int TextArgbColorInteger { get; set; }
    public ICollection<Event> Events { get; set; } 
        = new List<Event>();
}