namespace Calendars.Resources.Core;

/// <summary>
///     Exception handler interface.
/// </summary>
public interface IExceptionHandler
{
    Task HandleAsync<TException>(TException ex) 
        where TException : Exception;
}