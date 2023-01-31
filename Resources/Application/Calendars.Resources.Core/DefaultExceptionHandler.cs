using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;

namespace Calendars.Resources.Core;
/// <summary>
///     Exception handler implementation.
/// </summary>
public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
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