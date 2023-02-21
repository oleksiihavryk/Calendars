using Calendars.Resources.ActionResults;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calendars.Resources.Filters;
/// <summary>
///     Model state filter.
/// </summary>
public class CustomModelStateActionFilter : IAsyncActionFilter
{
    private readonly IResponseFactory _responseFactory;

    public CustomModelStateActionFilter(IResponseFactory responseFactory)
    {
        _responseFactory = responseFactory;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid == false)
        {
            var errors = context.ModelState
                .Where(kvp => 
                    kvp.Value != null && kvp.Value.Errors.Any())
                .ToDictionary(keySelector: kvp => kvp.Key,
                    elementSelector: kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage));
            var badRequestResult = new ResponseBadRequestResult(_responseFactory, errors);
            
            context.Result = badRequestResult;

            await Task.CompletedTask;
        }

        await next();
    }
}