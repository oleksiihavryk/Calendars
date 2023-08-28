namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of all clients in identity server in-memory configuration.
/// </summary>
public class ClientsConfiguration
{
    public virtual ClientConfiguration Resources { get; set; } = new ClientConfiguration();
    public virtual ClientConfiguration Web { get; set; } = new ClientConfiguration();
    public virtual ClientConfiguration Proxy { get; set; } = new ClientConfiguration();
    public virtual ClientConfiguration Authentication { get; set; } = new ClientConfiguration();
}