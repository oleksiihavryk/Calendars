using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Interface of service for requesting user resources from authentication server.
/// </summary>
public interface IUserAuthenticationService
{
    public Task<HttpResponseMessage> UpdateAsync(User user);
}