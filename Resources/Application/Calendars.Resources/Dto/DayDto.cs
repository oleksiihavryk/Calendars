using Calendars.Resources.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Calendars.Resources.Dto;
/// <summary>
///     Data transfer object of day.
/// </summary>
public class DayDto
{
    [Required] public Guid Id { get; set; }
    [Required] public Guid CalendarId { get; set; }
    [Required] public int DayNumber { get; set; }
    [Required] public int ArgbColorInteger { get; set; }
    public IEnumerable<Event> Events { get; set; }
}