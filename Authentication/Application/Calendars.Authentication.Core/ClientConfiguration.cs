namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of one client in identity server in-memory configuration.
/// </summary>
public class ClientConfiguration
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Secret { get; set; }
    public List<Uri> Origins { get; set; } = new List<Uri>();
    public List<string> Scopes { get; set; } = new List<string>();

    public ClientConfiguration(
        string id, 
        string name, 
        string secret)
    {
        Id = id;
        Name = name;
        Secret = secret;
    }
}