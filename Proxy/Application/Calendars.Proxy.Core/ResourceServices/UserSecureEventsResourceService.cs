using Calendars.Proxy.Core.Interfaces;
using System.Net;
using Calendars.Proxy.Domain;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace Calendars.Proxy.Core.ResourceServices;

/// <summary>
///     Service for requesting events from resource server but
///     with provided security from other users.
/// </summary>
public class UserSecureEventsResourceService : EventsResourceService
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

    public UserSecureEventsResourceService(
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
            var userId = JsonConvert.DeserializeObject<Event>(
                value: JsonConvert.DeserializeObject<Response>(
                        value: responseString)?.
                    Result?.
                    ToString() ?? string.Empty)?.UserId;

            if (await EventUserIdIsNotSecureAsync(userId))
                return UserSecurityIsPreventUnauthorizedAccessToResource();
        }

        return responseMessage;
    }
    public override async Task<HttpResponseMessage> SaveAsync(Event @event)
    {
        if (await EventUserIdIsNotSecureAsync(@event.UserId))
            return UserSecurityIsPreventUnauthorizedAccessToResource();

        return await base.SaveAsync(@event);
    }
    public override async Task<HttpResponseMessage> UpdateAsync(Event @event)
    {
        if (await EventUserIdIsNotSecureAsync(@event.UserId))
            return UserSecurityIsPreventUnauthorizedAccessToResource();

        return await base.UpdateAsync(@event);
    }
    public override async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        var responseMessage = await base.GetByIdAsync(id);

        if (responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadFromJsonAsync<Response>();
            var userId = JsonConvert.DeserializeObject<Event>(
                value: response?.Result?.ToString() ?? string.Empty)?.UserId;

            if (await EventUserIdIsNotSecureAsync(userId))
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
    private async Task<bool> EventUserIdIsNotSecureAsync(string? userId)
        => userId == null || await _userSecurityProviderService.IsSecureAsync(userId) == false;
}