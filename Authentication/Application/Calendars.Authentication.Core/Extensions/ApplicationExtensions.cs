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
            clientsConfiguration: new ClientsConfiguration 
            {
                Resources = new ClientConfiguration
                {
                    Id = configuration["Clients:Resources:Id"] ?? string.Empty,
                    Name = configuration["Clients:Resources:Name"] ?? string.Empty,
                    Secret = configuration["Clients:Resources:Secret"] ?? string.Empty,
                    Scopes = configuration
                        .GetSection("Clients:Resources:Scopes")
                        .Get<List<string>>() ?? new List<string>(),
                    Origins = configuration
                        .GetSection("Clients:Resources:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList() ?? new List<Uri>()
                },
                Web = new ClientConfiguration
                {
                    Id = configuration["Clients:Web:Id"] ?? string.Empty,
                    Name = configuration["Clients:Web:Name"] ?? string.Empty,
                    Secret = configuration["Clients:Web:Secret"] ?? string.Empty,
                    Scopes = configuration
                        .GetSection("Clients:Web:Scopes")
                        .Get<List<string>>() ?? new List<string>(),
                    Origins = configuration
                        .GetSection("Clients:Web:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList() ?? new List<Uri>()
                },
                Proxy = new ClientConfiguration
                {
                    Id = string.Empty,
                    Name = "Proxy",
                    Secret = string.Empty,
                    Origins = configuration
                        .GetSection("Clients:Proxy:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList() ?? new List<Uri>()
                },
                Authentication = new ClientConfiguration
                {
                    Id = configuration["Clients:Authentication:Id"] ?? string.Empty,
                    Name = configuration["Clients:Authentication:Name"] ?? string.Empty,
                    Secret = configuration["Clients:Authentication:Secret"] ?? string.Empty,
                    Scopes = configuration
                        .GetSection("Clients:Authentication:Scopes")
                        .Get<List<string>>() ?? new List<string>(),
                    Origins = configuration
                        .GetSection("Clients:Authentication:Origins")
                        .Get<List<string>>()?
                        .Select(u => new Uri(u))
                        .ToList() ?? new List<Uri>()
                }
            }, isDevelopment);
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