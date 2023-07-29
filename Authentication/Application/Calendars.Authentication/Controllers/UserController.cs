using Calendars.Authentication.Core;
using Calendars.Authentication.Core.Interfaces;
using Calendars.Authentication.Domain;
using Calendars.Authentication.ViewModels;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Calendars.Authentication.Controllers;
/// <summary>
///     User controller.
/// </summary>
[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
[Route("[controller]")]
public class UserController : ResponseSupportedControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(
        IResponseFactory responseFactory, 
        UserManager<User> userManager) 
        : base(responseFactory)
    {
        _userManager = userManager;
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserViewModel userModel)
    {
        var user = await _userManager.FindByIdAsync(userModel.Id.ToString());

        if (user == null) return UnknownIdentifier(userModel.Id);

        if (await IsUserWithSameNameExistAsync(userModel.Name)) 
            return UserWithSameNameAlreadyExist(userModel.Name);

        user.Email = userModel.Email;
        user.UserName = userModel.Name;

        if (userModel.Password != null)
        {
            var newPasswordHash = _userManager.PasswordHasher
                .HashPassword(user, userModel.Password);

            user.PasswordHash = newPasswordHash;
        }

        user.ChangeSecurityStamp();

        var result = await _userManager.UpdateAsync(user);

        await UpdateClaimsAsync(user);

        if (result.Succeeded) return EntityUpdated(userModel);

        return IdentityErrorsOccurred(result.Errors);
    }

    private async Task<bool> IsUserWithSameNameExistAsync(string name)
        => (await _userManager.FindByNameAsync(name)) != null;
    private async Task UpdateClaimsAsync(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        await UpdateUsernameClaimAsync(user, claims);
        await UpdateEmailClaimAsync(user, claims);
    }
    private async Task UpdateUsernameClaimAsync(User user, IEnumerable<Claim> claims)
    {
        var usernameClaim = claims.First(c => c.Type == JwtClaimTypes.Name) 
                            ?? throw new ApplicationException(
                                message: "Username claim cannot be null.");
        await _userManager.ReplaceClaimAsync(user, 
            usernameClaim, 
            new Claim(JwtClaimTypes.Name, user.UserName));
    }
    private async Task UpdateEmailClaimAsync(User user, IEnumerable<Claim> claims)
    {
        var emailClaim = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email);
        
        if (user.Email != null)
        {
            var newClaim = new Claim(JwtClaimTypes.Email, user.Email);

            if (emailClaim != null)
                await _userManager.ReplaceClaimAsync(user, emailClaim, newClaim);
            else await _userManager.AddClaimAsync(user, newClaim);
        }
        else if (emailClaim != null) 
            await _userManager.RemoveClaimAsync(user, emailClaim);
    }
}