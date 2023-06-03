using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendars.Proxy.Domain;
    
/// <summary>
///     Event of day. Like go to supermarket, make a cleanup etc.
/// </summary>
public class Event
{
    public Guid? Id { get; set; }
    public Guid? DayId { get; set; }
    public string? Name { get; set; }
    public int? HoursFrom { get; set; }
    public int? HoursTo { get; set; }
    public int? MinutesFrom { get; set; }
    public int? MinutesTo { get; set; }
    public string? Description { get; set; }
}