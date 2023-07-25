using System.Net;
using System.Net.Http.Json;
using System.Text;
using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;
using Newtonsoft.Json;

namespace Calendars.Proxy.Core.ResourceServices;
/// <summary>
///     Service for requesting days from resource server but
///     with provided security from other users.
/// </summary>
public class UserSecureDaysResourceService : DaysResourceService
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
    private readonly IUserSecurityProviderService _userSecurityProviderService;

    public UserSecureDaysResourceService(
        IResourcesService resourcesService, 
        IUserSecurityProviderService userSecurityProviderService) 
        : base(resourcesService)
    {
        _userSecurityProviderService = userSecurityProviderService;
    }

    public override async Task<HttpResponseMessage> GetByIdAsync(string id)
    {
        var responseMessage = await base.GetByIdAsync(id);

        if (responseMessage.IsSuccessStatusCode)
        {
            var responseStream = new MemoryStream();

            await responseMessage.Content.CopyToAsync(responseStream);

            var responseString = Encoding.UTF8.GetString(responseStream.ToArray());
            var userId = JsonConvert.DeserializeObject<Day>(
                value: JsonConvert.DeserializeObject<Response>(
                        value: responseString)?.
                    Result?.
                    ToString() ?? string.Empty)?.UserId;

            if (await DayUserIdIsNotSecureAsync(userId))
                return UserSecurityIsPreventUnauthorizedAccessToResource();
        }

        return responseMessage;
    }
    public override async Task<HttpResponseMessage> SaveAsync(Day day)
    {
        if (await DayUserIdIsNotSecureAsync(day.UserId))
            return UserSecurityIsPreventUnauthorizedAccessToResource();

        return await base.SaveAsync(day);
    }
    public override async Task<HttpResponseMessage> UpdateAsync(Day day)
    {
        if (await DayUserIdIsNotSecureAsync(day.UserId))
            return UserSecurityIsPreventUnauthorizedAccessToResource();

        return await base.UpdateAsync(day);
    }
    public override async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        var responseMessage = await base.GetByIdAsync(id);

        if (responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadFromJsonAsync<Response>();
            var userId = JsonConvert.DeserializeObject<Day>(
                value: response?.Result?.ToString() ?? string.Empty)?.UserId;

            if (await DayUserIdIsNotSecureAsync(userId))
                return UserSecurityIsPreventUnauthorizedAccessToResource();
        }

        return await base.DeleteAsync(id);
    }

    private HttpResponseMessage UserSecurityIsPreventUnauthorizedAccessToResource()
    {
        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        responseMessage.Content = JsonContent.Create(_unauthorizedResponse);
        return responseMessage;
    }
    private async Task<bool> DayUserIdIsNotSecureAsync(string? userId)
        => userId == null || await _userSecurityProviderService.IsSecureAsync(userId) == false;
}