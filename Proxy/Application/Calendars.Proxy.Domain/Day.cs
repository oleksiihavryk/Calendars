using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Calendars.Proxy.Domain;

/// <summary>
///     Special day of calendar (like some holiday, birthday, anniversary or etc.)
/// </summary>
public class Day
{
    public Guid? Id { get; set; }
    public Guid? CalendarId { get; set; }
    public int? DayNumber { get; set; }
    public int? ArgbColorInteger { get; set; } 
    public ICollection<Event>? Events { get; set; } 
}