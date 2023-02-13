namespace Calendars.Resources.Core.Interfaces;

/// <summary>
///     Exception handler interface.
/// </summary>
public interface IExceptionHandler
{
    Task HandleAsync<TException>(TException ex)
        where TException : Exception;
}