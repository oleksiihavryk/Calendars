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
    public Guid Id { get; set; } = Guid.Empty;
    [Required] public Guid CalendarId { get; set; }
    [Required] public int DayNumber { get; set; }
    [Required] public int BackgroundArgbColorInteger { get; set; }
    [Required] public int TextArgbColorInteger { get; set; }
    public IEnumerable<EventDto> Events { get; set; } = Enumerable.Empty<EventDto>();
}