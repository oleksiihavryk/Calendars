using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Calendars.Authentication.Core;
/// <summary>
///     Secret validator for validation token requests for PKCE code flow.
/// </summary>
public class PkceSecretValidator : ISecretValidator
{
    private readonly IdentityServerInMemoryConfiguration _isConfiguration;

    public PkceSecretValidator(IdentityServerInMemoryConfiguration isConfiguration)
    {
        _isConfiguration = isConfiguration;
    }

    public Task<SecretValidationResult> ValidateAsync(
        IEnumerable<Secret> secrets, 
        ParsedSecret parsedSecret)
    {
        var fail = Task.FromResult(new SecretValidationResult { Success = false });
        var success = Task.FromResult(new SecretValidationResult { Success = true });

        if (parsedSecret.Type != IdentityServerConstants.ParsedSecretTypes.NoSecret)
            return fail;

        var sharedSecrets = secrets.Where(
            s => s.Type == IdentityServerConstants.SecretTypes.SharedSecret);
        if (!sharedSecrets.Any())
            return fail;

        if (_isConfiguration.Clients
            .Any(c => c.RequirePkce && c.ClientId == parsedSecret.Id) == false)
            return fail;

        return success;
    }
}