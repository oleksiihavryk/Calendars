namespace Calendars.Authentication.Exceptions;
/// <summary>
///     Exception model of error which occurred when user is register account.
/// </summary>
public class UserRegistrationException : Exception
{
    public override string Message => base.Message ??
                                      "Unknown error when user register account.";

    public UserRegistrationException(string? message = null)
        : this(message, inner: null)
    {
    }
    public UserRegistrationException(string? message, Exception? inner)
        : base(message, inner) 
    {
    }
}