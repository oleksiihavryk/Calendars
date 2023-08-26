namespace Calendars.Authentication.Domain;
/// <summary>
///     User data.
/// </summary>
public class UserData
{
    public Guid UserId { get; set; }
    public string Name { get; set; } 
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}