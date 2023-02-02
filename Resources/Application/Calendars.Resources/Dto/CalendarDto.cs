using Calendars.Resources.Domain;
using System.ComponentModel.DataAnnotations;

namespace Calendars.Resources.Dto;
/// <summary>
///     Data transfer object of calendar class.
/// </summary>
public class CalendarDto
{
    public Guid Id { get; set; }
    [Required] public string UserId { get; set; }
    [Required] [StringLength(32)] public string Name { get; set; }
    [Required] public int Year { get; set; }
    [Required] public CalendarType Type { get; set; }
    public IEnumerable<Day> Days { get; set; }
}