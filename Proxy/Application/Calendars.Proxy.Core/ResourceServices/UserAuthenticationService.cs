using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.ResourceServices;
/// <summary>
///     Service for access to user resources.
/// </summary>
public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IAuthenticationResourcesService _authenticationServer;

    public UserAuthenticationService(IAuthenticationResourcesService authenticationServer)
    {
        _authenticationServer = authenticationServer;
    }

    public virtual async Task<HttpResponseMessage> UpdateAsync(User user)
        => await _authenticationServer.RequestResourceAsync(
            method: HttpMethod.Put,
            path: "/user",
            body: user);
}