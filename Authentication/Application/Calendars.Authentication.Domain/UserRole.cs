using Microsoft.AspNetCore.Identity;

namespace Calendars.Authentication.Domain;

public class UserRole : IdentityRole
{
    public Roles Role { get; }

    private UserRole(Roles role)
        : base(role.ToString())
    {
        Role = role;
    }

    public static UserRole CreateRole(Roles role)
    {
        if (Enum.IsDefined(role) == false)
            throw new ArgumentException(
                paramName: nameof(role),
                message: $"Passed parameter {nameof(role)} is not defined in system.");

        return new UserRole(role);
    }
}