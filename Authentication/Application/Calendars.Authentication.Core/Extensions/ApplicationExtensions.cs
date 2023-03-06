using Microsoft.Extensions.Configuration;

namespace Calendars.Authentication.Core.Extensions;
/// <summary>
///     Application extensions.
/// </summary>
public static class ApplicationExtensions
{
    public static IdentityServerInMemoryConfiguration AssembleIdentityServerInMemoryConfiguration(
        this IConfiguration configuration)
        => new IdentityServerInMemoryConfiguration(
            clientsConfiguration: new ClientsConfiguration(
                resources: new ClientConfiguration(
                    id: configuration["Clients:Resources:Id"] ?? string.Empty,
                    name: configuration["Clients:Resources:Name"] ?? string.Empty,
                    secret: configuration["Clients:Resources:Secret"] ?? string.Empty,
                    origin: configuration["Clients:Resources:Origin"] ?? string.Empty)
                    {
                        Scopes = configuration
                            .GetValue<List<string>>("Clients:Resources:Scopes")
                    },
                web: new ClientConfiguration(
                    id: configuration["Clients:Web:Id"] ?? string.Empty,
                    name: configuration["Clients:Web:Name"] ?? string.Empty,
                    secret: configuration["Clients:Web:Secret"] ?? string.Empty,
                    origin: configuration["Clients:Web:Origin"] ?? string.Empty)
                    {
                        Scopes = configuration
                            .GetValue<List<string>>("Clients:Web:Scopes")
                    }));
}