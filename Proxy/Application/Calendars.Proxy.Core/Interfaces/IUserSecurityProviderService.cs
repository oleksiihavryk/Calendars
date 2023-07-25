namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Interface of service which secure resources of users from other users.
/// </summary>
public interface IUserSecurityProviderService
{
    public Task<bool> IsSecureAsync(string entityUserId);
}