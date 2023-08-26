using Calendars.Proxy.Core.Exceptions;
using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Core.Options;
using Calendars.Proxy.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

        services.AddScoped<IUserSecurityProvider, UserSecurityProvider>();
        
        services.AddScoped<ICalendarsService>(sp =>
        {
            var resOptions = sp.GetRequiredService<IOptions<ResourcesServerOptions>>();
            var calendarsService = new CalendarsService(
                new UserAuthenticationService(
                    new BearerAuthenticationService(  
                        credentials: resOptions.Value.Credentials,
                        service: new Service(
                            clientFactory: sp.GetRequiredService<IHttpClientFactory>(),
                            baseUri: new Uri(resOptions.Value.Uri))),
                    userSecurityProvider: sp.GetRequiredService<IUserSecurityProvider>()));
            return calendarsService;
        });
        services.AddScoped<IDaysService>(sp =>
        {
            var resOptions = sp.GetRequiredService<IOptions<ResourcesServerOptions>>();
            var daysService = new DaysService(
                new UserAuthenticationService(
                    new BearerAuthenticationService(
                        credentials: resOptions.Value.Credentials,
                        service: new Service(
                            clientFactory: sp.GetRequiredService<IHttpClientFactory>(),
                            baseUri: new Uri(resOptions.Value.Uri))),
                    userSecurityProvider: sp.GetRequiredService<IUserSecurityProvider>()));
            return daysService;
        });
        services.AddScoped<IEventsService>(sp =>
        {
            var resOptions = sp.GetRequiredService<IOptions<ResourcesServerOptions>>();
            var eventsService = new EventsService(
                new UserAuthenticationService(
                    new BearerAuthenticationService(
                        credentials: resOptions.Value.Credentials,
                        service: new Service(
                            clientFactory: sp.GetRequiredService<IHttpClientFactory>(),
                            baseUri: new Uri(resOptions.Value.Uri))),
                    userSecurityProvider: sp.GetRequiredService<IUserSecurityProvider>()));
            return eventsService;
        });
        services.AddScoped<IUserService>(sp =>
        {
            var authOptions = sp.GetRequiredService<IOptions<AuthenticationServerOptions>>();
            var userService = new UserService(
                new UserAuthenticationService(
                    new BearerAuthenticationService(
                        credentials: authOptions.Value.Credentials,
                        service: new Service(
                            clientFactory: sp.GetRequiredService<IHttpClientFactory>(),
                            baseUri: new Uri(authOptions.Value.Uri))),
                    userSecurityProvider: sp.GetRequiredService<IUserSecurityProvider>()));
            return userService;
        });

        return services;
    }
    public static void AssembleAuthenticationOptions(
        this IConfiguration configuration,
        AuthenticationServerOptions opt)
    {
        opt.Uri = configuration["AuthenticationServer:Uri"] ?? 
                  throw new OptionsConfigurationException();
        opt.Credentials = new Credentials(new Uri(opt.Uri))
        {
            ClientId = configuration["AuthenticationServer:ClientId"] ??
                       throw new OptionsConfigurationException(),
            ClientSecret = configuration["AuthenticationServer:ClientSecret"] ??
                           throw new OptionsConfigurationException(),
            Scopes = configuration
                         .GetSection("AuthenticationServer:Scopes")
                         .Get<List<string>>() ??
                     throw new OptionsConfigurationException()
        };
    }
    public static void AssembleResourcesOptions(
        this IConfiguration configuration,
        ResourcesServerOptions opt)
    {
        opt.Uri = configuration["ResourcesServer:Uri"] ??
                  throw new OptionsConfigurationException();
        var authenticationServer = configuration["AuthenticationServer:Uri"] ?? 
                                   throw new OptionsConfigurationException();
        opt.Credentials = new Credentials(new Uri(authenticationServer))
        {
            ClientId = configuration["ResourcesServer:ClientId"] ??
                       throw new OptionsConfigurationException(),
            ClientSecret = configuration["ResourcesServer:ClientSecret"] ??
                           throw new OptionsConfigurationException(),
            Scopes = configuration
                         .GetSection("ResourcesServer:Scopes")
                         .Get<List<string>>() ??
                     throw new OptionsConfigurationException()
        };
    }
    public static void AssembleWebOptions(
        this IConfiguration configuration,
        WebServerOptions opt)
    {
        opt.Uri = configuration["WebServer:Uri"] ??
                  throw new OptionsConfigurationException();
    }
}