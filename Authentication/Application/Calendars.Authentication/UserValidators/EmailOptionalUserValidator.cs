using Microsoft.AspNetCore.Identity;

namespace Calendars.Authentication.UserValidators;
/// <summary>
///     User validator class for provide setting of optional user email to system.
/// </summary>
public class EmailOptionalUserValidator<TUser> : UserValidator<TUser> where TUser : class
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
    {
        var result = await base.ValidateAsync(manager, user);
        if (!result.Succeeded && string.IsNullOrWhiteSpace(await manager.GetEmailAsync(user)))
        {
            var errors = result.Errors
                .Where(e => e.Code != "InvalidEmail");
            return errors.Any() ?
                IdentityResult.Failed(errors.ToArray()) :
                IdentityResult.Success;
        }
        return result;
    }
}