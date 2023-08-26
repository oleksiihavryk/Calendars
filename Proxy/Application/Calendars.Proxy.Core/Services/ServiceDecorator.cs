using Calendars.Proxy.Core.Interfaces;

namespace Calendars.Proxy.Core.Services;
/// <summary>
///     Decorator class for IService interface.
/// </summary>
public abstract class ServiceDecorator : IService
{
    protected IService Service { get; set; }

    public HttpClient Client => Service.Client;

    protected ServiceDecorator(IService service)
    {
        ArgumentNullException.ThrowIfNull(service);

        Service = service;
    }

    public virtual async Task<HttpResponseMessage> RequestAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null)
        => await Service.RequestAsync(method, path, body, headers);
}