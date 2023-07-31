using Calendars.Proxy.ActionResults;
using Calendars.Proxy.Core.Interfaces;
using Calendars.Proxy.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Proxy.Controllers;
/// <summary>
///     User controller.
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : SimilarResponseSupportedControllerBase
{
    private readonly IUserAuthenticationService _userService;

    public UserController(IUserAuthenticationService userService)
    {
        _userService = userService;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] User user)
    {
        var response = await _userService.UpdateAsync(user);
        return SimilarResponse(response);
    }
}