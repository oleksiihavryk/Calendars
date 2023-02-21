using Calendars.Resources.ActionResults;
using Calendars.Resources.Core.Interfaces;
using Calendars.Resources.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace Calendars.Resources.Tests.Filters;

public class ModelStateValidatorActionFilterTests
{
    private readonly ActionExecutionDelegate _next = 
        () => Task.FromResult<ActionExecutedContext>(null!);
    private readonly IResponseFactory _responseFactory = 
        new Mock<IResponseFactory>().Object;

    [Fact]
    public async Task OnActionExecutionAsync_ModelStateIsEmpty_ResultIsNull()
    {
        //arrange
        var modelState = new ModelStateDictionary();
        var context = new ActionExecutingContext(
            actionContext: new ActionContext(
                httpContext: new Mock<HttpContext>().Object,
                routeData: new Mock<RouteData>().Object,
                actionDescriptor: new Mock<ActionDescriptor>().Object,
                modelState),
            filters: new List<IFilterMetadata>(),
            actionArguments: new Dictionary<string, object?>(),
            controller: new Mock<Controller>().Object);
        var validator = new CustomModelStateActionFilter(_responseFactory);
        //act
        await validator.OnActionExecutionAsync(context, _next);
        //assert
        Assert.Null(context.Result);
    }
    [Fact]
    public async Task OnActionExecutionAsync_ModelStateIsNotHaveAnError_ResultIsNull()
    {
        //arrange
        var modelState = new ModelStateDictionary();
        modelState.SetModelValue("1", "", "");
        modelState.TryGetValue("1", out var entry);
        if (entry != null) entry.ValidationState = ModelValidationState.Valid;
        var context = new ActionExecutingContext(
            actionContext: new ActionContext(
                httpContext: new Mock<HttpContext>().Object,
                routeData: new Mock<RouteData>().Object,
                actionDescriptor: new Mock<ActionDescriptor>().Object,
                modelState),
            filters: new List<IFilterMetadata>(),
            actionArguments: new Dictionary<string, object?>(),
            controller: new Mock<Controller>().Object);
        var validator = new CustomModelStateActionFilter(_responseFactory);
        //act
        await validator.OnActionExecutionAsync(context, _next);
        //assert
        Assert.Null(context.Result);
    }
    [Fact]
    public async Task OnActionExecutionAsync_ModelStateIsHaveAnError_ResultIsResponseBadRequest()
    {
        //arrange
        var modelState = new ModelStateDictionary();
        modelState.SetModelValue("1", "", "");
        modelState.AddModelError("1", "");
        var context = new ActionExecutingContext(
            actionContext: new ActionContext(
                httpContext: new Mock<HttpContext>().Object,
                routeData: new Mock<RouteData>().Object,
                actionDescriptor: new Mock<ActionDescriptor>().Object,
                modelState),
            filters: new List<IFilterMetadata>(),
            actionArguments: new Dictionary<string, object?>(),
            controller: new Mock<Controller>().Object);
        var validator = new CustomModelStateActionFilter(_responseFactory);
        //act
        await validator.OnActionExecutionAsync(context, _next);
        //assert
        Assert.NotNull(context.Result);
        Assert.IsType<ResponseBadRequestResult>(context.Result);
    }
}