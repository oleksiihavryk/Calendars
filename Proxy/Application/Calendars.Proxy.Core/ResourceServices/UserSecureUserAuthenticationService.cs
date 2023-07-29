using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;
using System.Net.Http.Json;
using System.Net;

namespace Calendars.Proxy.Core.ResourceServices;

/// <summary>
///     Service for requesting user info from authentication server but
///     with provided security from other users.
/// </summary>
public class UserSecureUserAuthenticationService : UserAuthenticationService
{
    private const string UnauthorizedResponseMessage = "Security error! User with current user id try " +
                                                       "to get access to resource of other user.";
    private readonly Response _unauthorizedResponse = new Response()
    {
        IsSuccess = false,
        Messages = new[] { UnauthorizedResponseMessage },
        Result = null,
        StatusCode = (int)HttpStatusCode.Unauthorized
    };
    private readonly IUserSecurityProviderService _userSecurityProvider;

    public UserSecureUserAuthenticationService(
        IAuthenticationResourcesService authenticationServer,
        IUserSecurityProviderService userSecurityProvider) 
        : base(authenticationServer)
    {
        _userSecurityProvider = userSecurityProvider;
    }

    public override async Task<HttpResponseMessage> UpdateAsync(User user)
    {
        if (await UserIdIsNotSecureAsync(user.Id.ToString()))
            return UserSecurityIsPreventUnauthorizedAccessToResource();

        return await base.UpdateAsync(user);
    }

    private HttpResponseMessage UserSecurityIsPreventUnauthorizedAccessToResource()
    {
        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        responseMessage.Content = JsonContent.Create(_unauthorizedResponse);
        return responseMessage;
    }
    private async Task<bool> UserIdIsNotSecureAsync(string? userId)
        => userId == null || await _userSecurityProvider.IsSecureAsync(userId) == false;
}