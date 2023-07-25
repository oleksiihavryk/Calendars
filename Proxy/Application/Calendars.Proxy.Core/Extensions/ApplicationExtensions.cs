using Calendars.Proxy.Core.Exceptions;
using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Calendars.Proxy.Core.ResourceServices;
using Calendars.Proxy.Core.ResourcesServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendars.Proxy.Core.Extensions;
/// <summary>
///     Application configurations.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Add core services for requesting resources from resource server.
    /// </summary>
    /// <returns>
    ///     Implementation of IServiceCollection interface after completing the operation.
    /// </returns>
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<ResourcesServerOptions>()
            .Configure(configuration.AssembleResourcesOptions);
        
        services.AddOptions<AuthenticationServerOptions>()
            .Configure(configuration.AssembleAuthenticationOptions);

        services.AddOptions<WebServerOptions>()
            .Configure(configuration.AssembleWebOptions);

        services.AddScoped<IResourcesService, AuthenticationResourcesService>();
        services.AddScoped<IUserSecurityProviderService, UserSecurityProviderService>();
        
        services.AddScoped<ICalendarsResourceService, UserSecureCalendarsResourceService>();
        services.AddScoped<IEventsResourceService, UserSecureEventsResourceService>();
        services.AddScoped<IDaysResourceService, UserSecureDaysResourceService>();
        
        return services;
    }

    public static void AssembleAuthenticationOptions(
        this IConfiguration configuration,
        AuthenticationServerOptions opt)
    {
        opt.Uri = configuration["AuthenticationServer:Uri"] ??
                  throw new OptionsConfigurationException();
        opt.ClientId = configuration["AuthenticationServer:ClientId"] ??
                       throw new OptionsConfigurationException();
        opt.ClientSecret = configuration["AuthenticationServer:ClientSecret"] ??
                           throw new OptionsConfigurationException();
        opt.Scopes = configuration
                         .GetSection("AuthenticationServer:Scopes")
                         .Get<List<string>>() ??
                     throw new OptionsConfigurationException();
    }
    public static void AssembleResourcesOptions(
        this IConfiguration configuration,
        ResourcesServerOptions opt)
    {
        opt.Uri = configuration["ResourcesServer:Uri"] ??
                  throw new OptionsConfigurationException();
    }
    public static void AssembleWebOptions(
        this IConfiguration configuration,
        WebServerOptions opt)
    {
        opt.Uri = configuration["WebServer:Uri"] ??
                  throw new OptionsConfigurationException();
    }
}