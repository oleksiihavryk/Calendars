using Calendars.Authentication.Core;
using Calendars.Authentication.Shared;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Moq;
using static IdentityServer4.IdentityServerConstants.StandardScopes;
using IdentityScopes = IdentityServer4.IdentityServerConstants.StandardScopes;

namespace Calendars.Authentication.Tests.Core;
public class IdentityServerInMemoryConfigurationTests
{
    private static readonly ClientConfiguration Auth = new()
    {
        Id = "auth123",
        Name = "auth",
        Origins = { new Uri("http://localhost:1") },
        Scopes = { IdentityServerConstants.LocalApi.ScopeName },
        Secret = "auth_secret"
    };
    private static readonly ClientConfiguration Res = new()
    {
        Id = "res123",
        Name = "res",
        Origins = { new Uri("http://localhost:2") },
        Scopes = { ApplicationIdentityServerConstants.ResourcesApiScopeName },
        Secret = "res"
    };
    private static readonly ClientConfiguration Web = new()
    {
        Id = "web123",
        Name = "web",
        Origins = { new Uri("http://localhost:3") },
        Scopes = { OpenId, Email, Profile, },
        Secret = "web_secret"
    };
    private static readonly ClientConfiguration Proxy = new()
    {
        Id = string.Empty,
        Name = "proxy",
        Origins = { new Uri("http://localhost:4") },
        Secret = ""
    };

    [Fact]
    public void ClientsOrigins_GetClientOrigins_ReturnsOriginsOfAllClients()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var origins = isConfiguration.ClientsOrigins;
        var expectedOrigins = clCfg.Resources.Origins
            .Concat(clCfg.Web.Origins)
            .Concat(clCfg.Proxy.Origins)
            .Select(u => u.OriginalString);
        //assert
        Assert.NotNull(expectedOrigins);
        Assert.IsType<string[]>(origins);
        foreach (var o in expectedOrigins)
            Assert.Contains(o, origins);
    }
    [Fact]
    public void Clients_GetClientsInDevelopmentMode_ReturnsAllClientsData()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var clients = isConfiguration.Clients;
        //assert
        //clients generic data check
        Assert.NotNull(clients);
        Assert.IsType<List<Client>>(clients);
        Assert.Equal(3, clients.Count);
        
        //client id`s check
        var clientIds = clients.Select(c => c.ClientId);
        foreach (var id in clientIds)
            Assert.Contains(id, new[] { Auth.Id, Res.Id, Web.Id });
        Assert.DoesNotContain(Proxy.Id, clientIds);

        //client names check
        var clientNames = clients.Select(c => c.ClientName);
        foreach (var name in clientNames)
            Assert.Contains(name, new[] { Auth.Name, Res.Name, Web.Name });
        Assert.DoesNotContain(Proxy.Name, clientNames);

        //client secret check
        var clientSecret = clients.SelectMany(
            c => c.ClientSecrets.Select(c => c.Value));
        foreach (var secret in clientSecret)
            Assert.Contains(secret, new[]
            {
                Auth.Secret.ToSha256(), 
                Res.Secret.ToSha256(),
                Web.Secret.ToSha256()
            });
        Assert.DoesNotContain(Proxy.Secret, clientSecret);

        //clients check
        var auth = clients.FirstOrDefault(c => c.ClientId == Auth.Id);
        Assert.NotNull(auth);
        Assert.IsType<Client>(auth);
        var web = clients.FirstOrDefault(c => c.ClientId == Web.Id);
        Assert.NotNull(web);
        Assert.IsType<Client>(web);
        var res = clients.FirstOrDefault(c => c.ClientId == Res.Id);
        Assert.NotNull(res);
        Assert.IsType<Client>(res);
        var proxy = clients.FirstOrDefault(c => c.ClientId == Proxy.Id);
        Assert.Null(proxy);

        //scopes check
        foreach (var scope in auth.AllowedScopes)
            Assert.Contains(scope, Auth.Scopes);
        foreach (var scope in res.AllowedScopes)
            Assert.Contains(scope, Res.Scopes);
        foreach (var scope in web.AllowedScopes)
            Assert.Contains(scope, Web.Scopes);

        //web generic data check
        Assert.True(web.RequirePkce);
        Assert.True(web.AlwaysIncludeUserClaimsInIdToken);
        Assert.True(web.AllowAccessTokensViaBrowser);
        Assert.Equal(GrantTypes.Code, web.AllowedGrantTypes);

        //auth generic data check
        Assert.Equal(isDevelopment == false, auth.RequirePkce);
        Assert.Equal(GrantTypes.ClientCredentials, auth.AllowedGrantTypes);
        Assert.True(auth.AlwaysIncludeUserClaimsInIdToken);

        //res generic data check
        Assert.Equal(isDevelopment == false, res.RequirePkce);
        Assert.Equal(GrantTypes.ClientCredentials, res.AllowedGrantTypes);
    }
    [Fact]
    public void Clients_GetClientsInNotDevelopmentMode_ReturnsAllClientsData()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var clients = isConfiguration.Clients;
        //assert
        //clients generic data check
        Assert.NotNull(clients);
        Assert.IsType<List<Client>>(clients);
        Assert.Equal(3, clients.Count);

        //client id`s check
        var clientIds = clients.Select(c => c.ClientId);
        foreach (var id in clientIds)
            Assert.Contains(id, new[] { Auth.Id, Res.Id, Web.Id });
        Assert.DoesNotContain(Proxy.Id, clientIds);

        //client names check
        var clientNames = clients.Select(c => c.ClientName);
        foreach (var name in clientNames)
            Assert.Contains(name, new[] { Auth.Name, Res.Name, Web.Name });
        Assert.DoesNotContain(Proxy.Name, clientNames);

        //client secret check
        var clientSecret = clients.SelectMany(
            c => c.ClientSecrets.Select(c => c.Value));
        foreach (var secret in clientSecret)
            Assert.Contains(secret, new[]
            {
                Auth.Secret.ToSha256(),
                Res.Secret.ToSha256(),
                Web.Secret.ToSha256()
            });
        Assert.DoesNotContain(Proxy.Secret, clientSecret);

        //clients check
        var auth = clients.FirstOrDefault(c => c.ClientId == Auth.Id);
        Assert.NotNull(auth);
        Assert.IsType<Client>(auth);
        var web = clients.FirstOrDefault(c => c.ClientId == Web.Id);
        Assert.NotNull(web);
        Assert.IsType<Client>(web);
        var res = clients.FirstOrDefault(c => c.ClientId == Res.Id);
        Assert.NotNull(res);
        Assert.IsType<Client>(res);
        var proxy = clients.FirstOrDefault(c => c.ClientId == Proxy.Id);
        Assert.Null(proxy);

        //scopes check
        foreach (var scope in auth.AllowedScopes)
            Assert.Contains(scope, Auth.Scopes);
        foreach (var scope in res.AllowedScopes)
            Assert.Contains(scope, Res.Scopes);
        foreach (var scope in web.AllowedScopes)
            Assert.Contains(scope, Web.Scopes);

        //web generic data check
        Assert.True(web.RequirePkce);
        Assert.True(web.AlwaysIncludeUserClaimsInIdToken);
        Assert.True(web.AllowAccessTokensViaBrowser);
        Assert.Equal(GrantTypes.Code, web.AllowedGrantTypes);

        //auth generic data check
        Assert.Equal(isDevelopment == false, auth.RequirePkce);
        Assert.Equal(GrantTypes.ClientCredentials, auth.AllowedGrantTypes);
        Assert.True(auth.AlwaysIncludeUserClaimsInIdToken);

        //res generic data check
        Assert.Equal(isDevelopment == false, res.RequirePkce);
        Assert.Equal(GrantTypes.ClientCredentials, res.AllowedGrantTypes);
    }
    [Fact]
    public void Scopes_GetScopes_ReturnsApiScopes()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var scopes = isConfiguration.Scopes;
        //assert
        Assert.NotNull(scopes);
        Assert.IsType<List<ApiScope>>(scopes);
        foreach (var name in scopes.Select(s => s.Name))
            Assert.Contains(name, new []
            {
                IdentityServerConstants.LocalApi.ScopeName, 
                ApplicationIdentityServerConstants.ResourcesApiScopeName
            });
    }
    [Fact]
    public void ApiResources_GetApiResources_ReturnsApiResources()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var apiResources = isConfiguration.ApiResources;
        //assert
        Assert.NotNull(apiResources);
        Assert.IsType<List<ApiResource>>(apiResources);
        foreach (var name in apiResources.Select(s => s.Name))
            Assert.Contains(name, new[]
            {
                IdentityServerConstants.LocalApi.ScopeName,
                ApplicationIdentityServerConstants.ResourcesApiScopeName
            });
    }
    [Fact]
    public void IdentityResources_GetIdentityResources_ReturnsIdentityResources()
    {
        //arrange
        var clientsConfiguration = new Mock<ClientsConfiguration>();
        clientsConfiguration.Setup(c => c.Authentication).Returns(Auth);
        clientsConfiguration.Setup(c => c.Resources).Returns(Res);
        clientsConfiguration.Setup(c => c.Web).Returns(Web);
        clientsConfiguration.Setup(c => c.Proxy).Returns(Proxy);
        var isDevelopment = false;
        var clCfg = clientsConfiguration.Object;
        var isConfiguration = new IdentityServerInMemoryConfiguration(clCfg, isDevelopment);
        //act
        var identityResources = isConfiguration.Resources;
        //assert
        Assert.NotNull(identityResources);
        Assert.IsType<List<IdentityResource>>(identityResources);
        foreach (var name in identityResources.Select(s => s.Name))
            Assert.Contains(name, new[] { Email, OpenId, Profile });
    }
}