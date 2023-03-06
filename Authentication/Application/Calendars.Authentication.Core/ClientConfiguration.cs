namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of one client in identity server in-memory configuration.
/// </summary>
public class ClientConfiguration
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Secret { get; set; }
    public Uri Origin { get; set; }
    
    public List<string> Scopes { get; set; } = new List<string>();

    public ClientConfiguration(
        string id, 
        string name, 
        string secret, 
        string origin)
    {
        Id = id;
        Name = name;
        Secret = secret;
        Origin = new Uri(origin);
    }
}