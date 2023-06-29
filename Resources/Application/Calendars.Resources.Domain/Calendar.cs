using System.ComponentModel.DataAnnotations;

namespace Calendars.Resources.Domain;
/// <summary>
///     Calendar - main operational unit in domain.
///     Model of calendar. 
/// </summary>
public class Calendar
{

    [Required] public Guid Id { get; set; }
    [Required] public string UserId { get; set; }

    [Required] public string Name { get; set; }
    [Required] public int Year { get; set; }
    [Required] public CalendarType Type { get; set; }
    public ICollection<Day> Days { get; set; } = new List<Day>();
}