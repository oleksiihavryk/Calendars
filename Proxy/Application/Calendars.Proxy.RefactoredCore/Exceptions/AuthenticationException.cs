namespace Calendars.Proxy.RefactoredCore.Exceptions;
/// <summary>
///     Exception which occurred when unknown error has occurred on authentication server side.
/// </summary>
public class AuthenticationException : Exception
{
    public override string Message => base.Message ??
                                      "Unknown error has occurred on " +
                                      "authentication server side.";

    public AuthenticationException(string? message = null)
        : this(message, null)
    {
    }
    public AuthenticationException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}