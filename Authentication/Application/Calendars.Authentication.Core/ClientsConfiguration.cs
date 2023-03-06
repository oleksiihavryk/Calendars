namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of all clients in identity server in-memory configuration.
/// </summary>
public class ClientsConfiguration
{
    public ClientConfiguration Resources { get; set; }
    public ClientConfiguration Web { get; set; }

    public ClientsConfiguration(
        ClientConfiguration resources,
        ClientConfiguration web)
    {
        Resources = resources;
        Web = web;
    }
}