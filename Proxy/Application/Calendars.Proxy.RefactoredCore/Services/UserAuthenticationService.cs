using Calendars.Proxy.RefactoredCore.Interfaces;

namespace Calendars.Proxy.RefactoredCore.Services;
/// <summary>
///     Implementation of service decorator for requesting secure resources from external server.
/// </summary>
public class UserAuthenticationService : ServiceDecorator
{
    private readonly IUserSecurityProvider _userSecurityProvider;

    public UserAuthenticationService(
        IService service,
        IUserSecurityProvider userSecurityProvider) 
        : base(service)
    {
        ArgumentNullException.ThrowIfNull(userSecurityProvider);

        _userSecurityProvider = userSecurityProvider;
    }

    public override Task<HttpResponseMessage> RequestAsync(
        HttpMethod method, 
        string? path = null, 
        object? body = null, 
        IDictionary<string, string>? headers = null)
    {
        return base.RequestAsync(method, path, body, headers);
    }
}