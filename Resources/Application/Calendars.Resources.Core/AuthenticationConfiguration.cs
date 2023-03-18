namespace Calendars.Resources.Core;
/// <summary>
///     Authentication data from configuration file.
/// </summary>
public class AuthenticationConfiguration
{
    public string Scope { get; }
    public Uri Uri { get; }

    public AuthenticationConfiguration(
        string uri, 
        string scope)
    {
        Scope = scope;
        Uri = new Uri(uri);
    }
}