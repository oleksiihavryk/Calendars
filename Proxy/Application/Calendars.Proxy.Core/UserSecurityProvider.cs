using System.IdentityModel.Tokens.Jwt;
using Calendars.Proxy.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Calendars.Proxy.Core;
/// <summary>
///     Service which secure resources of users from other users.
/// </summary>
public class UserSecurityProvider : IUserSecurityProvider
{
    private readonly HttpContext _httpContext;

    private string UserId
    {
        get
        {
            var token = _httpContext.Request.Headers.Authorization.First();

            ArgumentNullException.ThrowIfNull(token);

            var securityToken = new JwtSecurityTokenHandler().ReadToken(token.Substring(7));
            var userId = (securityToken as JwtSecurityToken)?
                .Claims
                .First(c => c.Type == "sub").Value;

            ArgumentNullException.ThrowIfNull(userId);

            return userId;
        }
    } 

    public UserSecurityProvider(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);

        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<bool> IsSecureAsync(string entityUserId)
        => await Task.FromResult(
            result: string.Equals(
                a: entityUserId, 
                b: UserId,
                comparisonType: StringComparison.Ordinal));
}