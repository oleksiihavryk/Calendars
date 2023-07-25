namespace Calendars.Proxy.Core.Interfaces;

/// <summary>
///     Interface of service for requesting resources from resource server.
/// </summary>
public interface IResourcesService
{
    Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null);
}