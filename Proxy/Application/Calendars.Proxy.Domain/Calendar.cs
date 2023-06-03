using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendars.Proxy.Domain;
/// <summary>
///     Calendar - main operational unit in domain.
///     Model of calendar. 
/// </summary>
public class Calendar
{
    public Guid? Id { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public CalendarType? Type { get; set; }
    public ICollection<Day>? Days { get; set; }
}