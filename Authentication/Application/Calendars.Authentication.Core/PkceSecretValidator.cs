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

    public virtual async Task<SecretValidationResult> ValidateAsync(
        IEnumerable<Secret> secrets, 
        ParsedSecret parsedSecret)
    {
        var fail = await Task.FromResult(
            new SecretValidationResult { Success = false });
        var success = await Task.FromResult(
            new SecretValidationResult { Success = true });

        if (parsedSecret.Type != IdentityServerConstants.ParsedSecretTypes.NoSecret)
            return fail;

        if (secrets.Any(s => 
                s.Type == IdentityServerConstants.SecretTypes.SharedSecret) 
            == false) return fail;

        if (_isConfiguration.Clients
            .Any(c => c.RequirePkce && c.ClientId == parsedSecret.Id) == false)
            return fail;

        return success;
    }
}