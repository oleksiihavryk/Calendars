namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of all clients in identity server in-memory configuration.
/// </summary>
public class ClientsConfiguration
{
    public ClientConfiguration Resources { get; set; } = new ClientConfiguration();
    public ClientConfiguration Web { get; set; } = new ClientConfiguration();
    public ClientConfiguration Proxy { get; set; } = new ClientConfiguration();
    public ClientConfiguration Authentication { get; set; } = new ClientConfiguration();
}