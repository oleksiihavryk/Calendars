using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Calendars.Proxy.Core.ResourcesServices;
/// <summary>
///     Abstract service for requesting resources from authentication server.
/// </summary>
public class BaseAuthenticationResourcesService : IAuthenticationResourcesService
{
    protected string BaseUri { get; set; }
    protected HttpClient Client { get; set; }

    public BaseAuthenticationResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<AuthenticationServerOptions> options)
    {
        Client = clientFactory.CreateClient();
        BaseUri = options.Value.Uri;
    }

    public virtual Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null)
    {
        var message = new HttpRequestMessage(
            method,
            requestUri: BaseUri + (path ?? string.Empty));

        if (body != null)
            message.Content = JsonContent.Create(body);

        if (headers != null)
            foreach (var kvp in headers)
                message.Headers.Add(kvp.Key, kvp.Value);

        return Client.SendAsync(message);
    }
}