using Calendars.Authentication.ActionResults;
using Calendars.Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Authentication.Controllers;
/// <summary>
///     ControllerBase derived class which support API responses with response object.
/// </summary>
public class ResponseSupportedControllerBase : ControllerBase
{
    private readonly IResponseFactory _responseFactory;

    public ResponseSupportedControllerBase(IResponseFactory responseFactory)
    {
        _responseFactory = responseFactory;
    }
    
    [NonAction] protected ResponseNotFoundResult UnknownIdentifier<T>(T identifier)
        => new ResponseNotFoundResult(
            responseFactory: _responseFactory,
            value: null,
            messages: $"User with identifier {identifier} is not found.");
    [NonAction]
    protected ResponseBadRequestResult UserWithSameNameAlreadyExist(string name)
        => new ResponseBadRequestResult(
            responseFactory: _responseFactory,
            parameterAndErrors: new Dictionary<string, IEnumerable<string>>{ 
                ["Name"] = new string[] { $"User with name {name} is already exist." }
            });
    [NonAction] protected ResponseOkResult EntityUpdated<T>(T entity)
        => new ResponseOkResult(
            responseFactory: _responseFactory,
            value: entity,
            messages: "Entity is successfully updated in system with new values.");
    [NonAction]
    public ResponseIdentityErrorsResult IdentityErrorsOccurred(IEnumerable<IdentityError> errors)
        => new ResponseIdentityErrorsResult(
            responseFactory: _responseFactory,
            errors: errors);
}