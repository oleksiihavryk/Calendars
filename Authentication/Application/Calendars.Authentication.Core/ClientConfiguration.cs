namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of one client in identity server in-memory configuration.
/// </summary>
public class ClientConfiguration
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public List<Uri> Origins { get; set; } = new List<Uri>();
    public List<string> Scopes { get; set; } = new List<string>();
}