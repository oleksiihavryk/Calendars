namespace Calendars.Resources.Core;
/// <summary>
///     Authentication data from configuration file.
/// </summary>
public class AuthenticationConfiguration
{
    public string Audience { get; }
    public string Scope { get; }
    public Uri Uri { get; }

    public AuthenticationConfiguration(
        string uri, 
        string audience,
        string scope)
    {
        Audience = audience;
        Uri = new Uri(uri);
        Scope = scope;
    }
}