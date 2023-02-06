using Calendars.Resources.ActionResults;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Calendars.Resources.Controllers;
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

    public IActionResult EntityFound<T>(T entity)
        => new ResponseOkObjectResult(
            responseFactory: _responseFactory,
            value: entity,
            messages: "Entity is successfully find in system.");
    public IActionResult UnknownIdentifier<T>(T identifier)
        => new ResponseNotFoundResult(
            responseFactory: _responseFactory,
            value: identifier,
            messages: $"Entity with identifier {identifier} is not found.");
    public IActionResult EntityCreated<T>(T entity)
        => new ResponseCreatedResult(
            responseFactory: _responseFactory,
            value: entity,
            messages: "Object is successfully created by route " +
                      HttpContext.Request.GetDisplayUrl());
    public IActionResult EntityUpdated<T>(T entity)
        => new ResponseOkObjectResult(
            responseFactory: _responseFactory,
            value: entity,
            messages: "Entity is successfully updated in system with new values.");
    public IActionResult EntityDeleted()
        => new ResponseOkObjectResult(
            responseFactory: _responseFactory,
            value: null,
            messages: "Entity is successfully deleted from system.");
}