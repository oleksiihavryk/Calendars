namespace Calendars.Proxy.Core.Options;
/// <summary>
///     Authentication server external information.
/// </summary>
public class AuthenticationServerOptions
{
    private Uri? _uri = null;

    public string Uri
    {
        get => _uri?.OriginalString ?? string.Empty;
        set
        {
            if (System.Uri.TryCreate(value, UriKind.Absolute, out var res))
            {
                _uri = res;
            }
        }
    }
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public IEnumerable<string> Scopes { get; set; } = Enumerable.Empty<string>();
}