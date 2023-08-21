using Calendars.Authentication.Domain;

namespace Calendars.Authentication.Tests.Domain;
public class UserRoleTests
{
    [Fact]
    public void CreateRole_PassUndefinedRole_ThrowsArgumentException()
    {
        //arrange
        var role = (Roles)1000;
        //act
        void Act()
        {
            var userRole = UserRole.CreateRole(role);
        }
        //assert
        Assert.Throws<ArgumentException>(Act);
    }
    [Fact]
    public void CreateRole_PassCorrectRole_ReturnsUserRole()
    {
        //arrange
        var role = Roles.Admin;
        //act
        var userRole = UserRole.CreateRole(role);
        //assert
        Assert.NotNull(userRole);
        Assert.IsType<UserRole>(userRole);
        Assert.Equal(role, userRole.Role);
        Assert.Equal(role.ToString(), userRole.Name);
    }
}