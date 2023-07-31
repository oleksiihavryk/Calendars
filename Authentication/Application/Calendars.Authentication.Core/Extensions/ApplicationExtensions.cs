using Calendars.Authentication.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendars.Authentication.Core.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
public static class ApplicationExtensions
{
    public static IdentityServerInMemoryConfiguration AssembleIdentityServerInMemoryConfiguration(
        this IConfiguration configuration,
        bool isDevelopment)
        => new IdentityServerInMemoryConfiguration(
            clientsConfiguration: new ClientsConfiguration(
                resources: new ClientConfiguration(
                    id: configuration["Clients:Resources:Id"] ?? string.Empty,
                    name: configuration["Clients:Resources:Name"] ?? string.Empty,
                    secret: configuration["Clients:Resources:Secret"] ?? string.Empty)
                    {
                        Scopes = configuration
                            .GetSection("Clients:Resources:Scopes")
                            .Get<List<string>>(),
                        Origins = configuration
                            .GetSection("Clients:Resources:Origins")
                            .Get<List<string>>()?
                            .Select(u => new Uri(u))
                            .ToList()
                },
                web: new ClientConfiguration(
                    id: configuration["Clients:Web:Id"] ?? string.Empty,
                    name: configuration["Clients:Web:Name"] ?? string.Empty,
                    secret: configuration["Clients:Web:Secret"] ?? string.Empty)
                    {
                        Scopes = configuration
                            .GetSection("Clients:Web:Scopes")
                            .Get<List<string>>(),
                        Origins = configuration
                            .GetSection("Clients:Web:Origins")
                            .Get<List<string>>()?
                            .Select(u => new Uri(u))
                            .ToList()
                },
                proxy: new ClientConfiguration(
                    id: string.Empty,
                    name: "Proxy",
                    secret: string.Empty)
                {
                    Origins = configuration
                        .GetSection("Clients:Proxy:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList()
                },
                authentication: new ClientConfiguration(
                    id: configuration["Clients:Authentication:Id"] ?? string.Empty,
                    name: configuration["Clients:Authentication:Name"] ?? string.Empty,
                    secret: configuration["Clients:Authentication:Secret"] ?? string.Empty)
                {
                    Scopes = configuration
                        .GetSection("Clients:Authentication:Scopes")
                        .Get<List<string>>(),
                    Origins = configuration
                        .GetSection("Clients:Authentication:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList()
                }),
            isDevelopment);
    /// <summary>
    ///     Add response factory in DI Container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    ///     IServiceCollection implementation after completing the operation.
    /// </returns>
    public static IServiceCollection AddResponseFactory(this IServiceCollection services)
        => services.AddSingleton<IResponseFactory, ResponseFactory>();
}