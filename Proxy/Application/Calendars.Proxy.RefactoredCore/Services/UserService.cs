using Calendars.Proxy.Domain;
using Calendars.Proxy.RefactoredCore.Interfaces;

namespace Calendars.Proxy.RefactoredCore.Services;
/// <summary>
///     Service for requesting user endpoints on authentication server.
/// </summary>
public class UserService : ServiceDecorator, IUserService
{
    public UserService(IService service) 
        : base(service) { }

    public virtual async Task<HttpResponseMessage> UpdateAsync(User user)
        => await RequestAsync(
            method: HttpMethod.Put,
            path: "/user",
            body: user);
}