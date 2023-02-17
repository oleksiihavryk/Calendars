using Microsoft.AspNetCore.Identity;

namespace Calendars.Authentication.Domain;
/// <summary>
///     User model.
/// </summary>
public class User : IdentityUser
{
    public User(string userName)
        : base(userName)
    {
    }

    public void ChangeSecurityStamp()
    {
        SecurityStamp = Guid.NewGuid().ToString();
    }
}