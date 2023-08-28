namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Interface of user security provider which can check
///     user id`s of authenticated token request.
/// </summary>
public interface IUserSecurityProvider
{
    public Task<bool> IsSecureAsync(string entityUserId);
}