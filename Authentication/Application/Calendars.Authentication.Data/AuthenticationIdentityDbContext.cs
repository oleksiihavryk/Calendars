using Calendars.Authentication.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Calendars.Authentication.Data;
/// <summary>
///     Authentication server database context.
/// </summary>
public class AuthenticationIdentityDbContext : IdentityDbContext<User, UserRole, string>
{
    public AuthenticationIdentityDbContext(
        DbContextOptions<AuthenticationIdentityDbContext> options)   
        : base(options)
    {
    }
}