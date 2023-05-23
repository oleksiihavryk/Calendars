using System.Net.Http.Json;
using Calendars.Proxy.Core.Options;
using Microsoft.Extensions.Options;

namespace Calendars.Proxy.Core;
/// <summary>
///     Abstract service for requesting resources from resource server.
/// </summary>
public abstract class BaseResourcesService
{
    private readonly string _baseUri;
    
    protected HttpClient Client { get; set; }

    protected BaseResourcesService(
        IHttpClientFactory clientFactory,
        IOptions<ResourcesServerOptions> opts)
    {
        Client = clientFactory.CreateClient();
        _baseUri = opts.Value.Uri;
    }

    public virtual Task<HttpResponseMessage> RequestResourceAsync(
        HttpMethod method,
        string? path,
        object? body,
        IDictionary<string, string>? headers)
    {
        var message = new HttpRequestMessage(
            method,
            requestUri: _baseUri + (path ?? string.Empty));

        if (body != null) 
            message.Content = JsonContent.Create(body);

        if (headers != null)
            foreach (var kvp in headers)
                message.Headers.Add(kvp.Key, kvp.Value);
        
        return Client.SendAsync(message);
    }
}