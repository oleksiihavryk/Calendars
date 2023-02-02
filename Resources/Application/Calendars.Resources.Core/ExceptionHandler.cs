using Calendars.Resources.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Calendars.Resources.Core;
/// <summary>
///     Exception handler implementation.
/// </summary>
public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync<TException>(TException ex) 
        where TException : Exception
    {
        _logger.LogError(ex, message: "Unknown error has occurred in system.");
        await Task.CompletedTask;
    }
}