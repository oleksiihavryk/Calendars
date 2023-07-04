using Calendars.Authentication.Domain;
using Microsoft.AspNetCore.Identity;
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
        Database.EnsureCreated();
    }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<UserRole>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("Logins");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserToken<string>>().ToTable("Tokens");
    }
}