using Calendars.Proxy.Core.Interfaces;
using System.Net.Http.Json;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Calendars.Proxy.Core.Services;
/// <summary>
///     Implementation of service decorator for requesting secure resources from external server.
/// </summary>
public class UserAuthenticationService : ServiceDecorator
{
    private const string UnauthorizedResponseMessage = "Security error! User with current user id try " +
                                                       "to get access to resource of other user.";

    private readonly IUserSecurityProvider _userSecurityProvider;
    private readonly Response _unauthorizedResponse = new()
    {
        IsSuccess = false,
        Messages = new[] { UnauthorizedResponseMessage },
        Result = null,
        StatusCode = (int)HttpStatusCode.Unauthorized
    };

    public UserAuthenticationService(
        IService service,
        IUserSecurityProvider userSecurityProvider) 
        : base(service)
    {
        ArgumentNullException.ThrowIfNull(userSecurityProvider);

        _userSecurityProvider = userSecurityProvider;
    }

    public override async Task<HttpResponseMessage> RequestAsync(
        HttpMethod method, 
        string? path = null, 
        object? body = null, 
        IDictionary<string, string>? headers = null)
    {
        string? userId;

        if (path is not null)
            userId = new UserIdFinder().
                TryFindUserIdFromBody(body).
                TryFindUserIdFromPath(path).
                TryFindUserIdFromQuery(path);
        else userId = new UserIdFinder().TryFindUserIdFromBody(body);


        if (await CalendarUserIdIsNotSecureAsync(userId))
            return UserSecurityIsPreventUnauthorizedAccessToResource();
        
        return await base.RequestAsync(method, path, body, headers);
    }

    private HttpResponseMessage UserSecurityIsPreventUnauthorizedAccessToResource()
    {
        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        responseMessage.Content = JsonContent.Create(_unauthorizedResponse);
        return responseMessage;
    }
    private async Task<bool> CalendarUserIdIsNotSecureAsync(string? userId)
        => userId == null || (await _userSecurityProvider.IsSecureAsync(userId)) == false;

    /// <summary>
    ///     Helper class which helps find a user id from different parts of requests
    ///     as a methods chain.
    /// </summary>
    private class UserIdFinder
    {
        private string UserId { get; set; } = string.Empty;

        public UserIdFinder TryFindUserIdFromBody(object? body)
        {
            if (string.IsNullOrWhiteSpace(UserId) == false) return this;

            var objectString = JsonConvert.SerializeObject(body);

            var haveExplicitUserIdRegex = new Regex("\"UserId\":\"(\\d|\\w|-)+(\",|\"})");
            
            if (haveExplicitUserIdRegex.IsMatch(objectString))
            {
                var matchString = haveExplicitUserIdRegex.Match(objectString);
                var res = matchString.Value.Substring(
                    10,
                    matchString.Value.Length - 12);
                return res;
            }

            return objectString is "null" ? string.Empty : objectString;
        }
        public UserIdFinder TryFindUserIdFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(UserId) == false) return this;

            var startIndex = path.IndexOf("/user-id/", StringComparison.Ordinal);

            if (startIndex == -1) return this;

            //9 here because it is length of "/user-id/ and"
            startIndex += 9;

            var endIndex = path.IndexOf('&', startIndex + 1);

            endIndex = endIndex == -1 ? path.Length - 1 : endIndex;

            var res = path.Substring(
                startIndex,
                length: path.Length - startIndex - (path.Length - endIndex) + 1);
            return res;
        }
        public UserIdFinder TryFindUserIdFromQuery(string path)
        {
            if (string.IsNullOrWhiteSpace(UserId) == false) return this;

            var startIndex = path.IndexOf("userId=", StringComparison.Ordinal);

            if (startIndex == -1) return this;

            //9 here because it is length of "userId="
            startIndex += 7;

            var endIndex = path.IndexOf('&', startIndex + 1);

            endIndex = endIndex == -1 ? path.Length - 1 : endIndex;

            var res = path.Substring(
                startIndex,
                length: path.Length - startIndex - (path.Length - endIndex) + 1);
            return res;
        }

        public static implicit operator string (UserIdFinder userIdFinder)
        {
            return userIdFinder.UserId;
        }
        public static implicit operator UserIdFinder(string userId)
        {
            return new UserIdFinder()
            {
                UserId = userId
            };
        }
    }
}