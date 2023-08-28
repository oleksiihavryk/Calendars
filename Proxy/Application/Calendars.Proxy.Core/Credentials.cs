namespace Calendars.Proxy.Core;
/// <summary>
///     Credentials for get access to secure resources on external server.
/// </summary>
public class Credentials
{
    public Uri AuthenticationServer { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public IEnumerable<string> Scopes { get; set; } = Enumerable.Empty<string>();

    public Credentials(Uri authenticationServer)
    {
        AuthenticationServer = authenticationServer;
    }
}