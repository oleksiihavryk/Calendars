namespace Calendars.Proxy.Core.Options;
/// <summary>
///     Web server external information.
/// </summary>
public class WebServerOptions
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
}