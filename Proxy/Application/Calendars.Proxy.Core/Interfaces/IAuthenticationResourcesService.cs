namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Interface of service for requesting resources from authentication server.
/// </summary>
public interface IAuthenticationResourcesService
{
    Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null);
}