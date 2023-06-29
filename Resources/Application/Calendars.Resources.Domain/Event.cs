using System.ComponentModel.DataAnnotations;

namespace Calendars.Resources.Domain;

/// <summary>
///     Event of day. Like go to supermarket, make a cleanup etc.
/// </summary>
public class Event
{
    [Required] public Guid Id { get; set; }
    [Required] public Guid DayId { get; set; }

    [Required]public string Name { get; set; }
    [Required] public int HoursFrom { get; set; }
    [Required] public int HoursTo { get; set; }
    [Required] public int MinutesFrom { get; set; }
    [Required] public int MinutesTo { get; set; }
    public string? Description { get; set; }
}