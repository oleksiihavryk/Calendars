namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Interface of service for requesting resources from external server.
/// </summary>
public interface IService
{
    public HttpClient Client { get; }

    Task<HttpResponseMessage> RequestAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null);
}