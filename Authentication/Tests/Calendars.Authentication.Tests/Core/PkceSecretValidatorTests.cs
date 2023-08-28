using Calendars.Authentication.Core;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Moq;

namespace Calendars.Authentication.Tests.Core;
public class PkceSecretValidatorTests
{
    private const string ClientId = "123";

    private static readonly Mock<ClientsConfiguration> ClientsConfig = new();

    [Fact]
    public async Task ValidateAsync_IsParsedSecretValidatingWithEnabledPkce_ReturnsSuccessResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new [] { secret }, 
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_EmptyClients_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>());
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_IsMismatchClientWithWrongClientId_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId + " "
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_IsMismatchClientWithNonRequiredPkce_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = false,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_IsMismatchClientWithWrongParsedSecretId_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId + " s",
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_IsEmptySecrets_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: Array.Empty<Secret>(),
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_IsNotPassedSharedSecret_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.JsonWebKey
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.NoSecret,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
    [Fact]
    public async Task ValidateAsync_ParsedSecretIsJwtBearerType_ReturnsFailedResult()
    {
        //arrange
        var isConfig = new Mock<IdentityServerInMemoryConfiguration>(
            ClientsConfig.Object, false);
        isConfig.Setup(p => p.Clients)
            .Returns(new List<Client>
            {
                new Client()
                {
                    RequirePkce = true,
                    ClientId = ClientId
                }
            });
        var validator = new PkceSecretValidator(isConfig.Object);
        var secret = new Secret
        {
            Type = IdentityServerConstants.SecretTypes.SharedSecret
        };
        var parsedSecret = new ParsedSecret
        {
            Id = ClientId,
            Type = IdentityServerConstants.ParsedSecretTypes.JwtBearer,
        };
        //act
        var result = await validator.ValidateAsync(
            secrets: new[] { secret },
            parsedSecret);
        //assert
        Assert.NotNull(result);
        Assert.IsType<SecretValidationResult>(result);
        Assert.False(result.Success);
    }
}