using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendars.Resources.Domain;

/// <summary>
///     Event of day. Like go to supermarket, make a cleanup etc.
/// </summary>
public class Event
{
    [Key] public Guid Id { get; set; }
    [ForeignKey(name: "Day")] public Guid DayId { get; set; }

    [Required] [StringLength(32)] public string Name { get; set; }
    [Required] public int HoursFrom { get; set; }
    [Required] public int HoursTo { get; set; }
    [Required] public int MinutesFrom { get; set; }
    [Required] public int MinutesTo { get; set; }
    [StringLength(128)] public string? Description { get; set; }
}