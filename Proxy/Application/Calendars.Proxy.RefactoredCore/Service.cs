using Calendars.Proxy.RefactoredCore.Interfaces;
using System.Net.Http.Json;

namespace Calendars.Proxy.RefactoredCore;
/// <summary>
///     Default implementation of service for requesting resources from external server.
/// </summary>
public class Service : IService
{
    private readonly string _baseUri;

    public virtual HttpClient Client { get; }

    public Service(
        IHttpClientFactory clientFactory,
        Uri baseUri)
    {
        ArgumentNullException.ThrowIfNull(clientFactory);
        ArgumentNullException.ThrowIfNull(baseUri);

        Client = clientFactory.CreateClient();
        _baseUri = baseUri.AbsoluteUri;
    }

    public virtual Task<HttpResponseMessage> RequestAsync(
        HttpMethod method,
        string? path = null,
        object? body = null,
        IDictionary<string, string>? headers = null)
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