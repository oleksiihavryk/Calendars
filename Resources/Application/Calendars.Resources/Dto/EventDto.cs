using System.ComponentModel.DataAnnotations;

namespace Calendars.Resources.Dto;
/// <summary>
///     Data transfer object of event.
/// </summary>
public class EventDto
{
    public Guid Id { get; set; } = Guid.Empty;
    [Required] public Guid DayId { get; set; }
    [Required] public string UserId { get; set; }
    [Required][StringLength(32)] public string Name { get; set; }
    [Required] public int HoursFrom { get; set; }
    [Required] public int HoursTo { get; set; }
    [Required] public int MinutesFrom { get; set; }
    [Required] public int MinutesTo { get; set; }
    [StringLength(128)] public string? Description { get; set; }
}