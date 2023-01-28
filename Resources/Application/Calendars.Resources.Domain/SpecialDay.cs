using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Calendars.Resources.Domain;

/// <summary>
///     Special day of calendar (like some holiday, birthday, anniversary or etc.)
/// </summary>
public class SpecialDay
{
    public const KnownColor DefaultColor = KnownColor.Red;

    [Key] public Guid Id { get; set; }
    [ForeignKey(name: "Calendar")] public Guid CalendarId { get; set; }
    
    [Required] public int Day { get; set; }
    public int ArgbColorInteger { get; set; } = Color.FromKnownColor(DefaultColor).ToArgb();
    [NotMapped]
    public Color Color
    {
        get => Color.FromArgb(ArgbColorInteger);
        set => value.ToArgb();
    }
    public IEnumerable<DayEvent> Events = Enumerable.Empty<DayEvent>();
}