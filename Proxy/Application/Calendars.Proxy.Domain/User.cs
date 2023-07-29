namespace Calendars.Proxy.Domain;
/// <summary>
///     User data.
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}