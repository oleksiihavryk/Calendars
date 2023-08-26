namespace Calendars.Proxy.Core.Exceptions;
/// <summary>
///     Exception which occurred when options file is incorrect compile by main configuration file.
/// </summary>
public class OptionsConfigurationException : Exception
{
    public override string Message => base.Message ??
                                      "Options file is incorrect compile" +
                                      " by main configuration file.";

    public OptionsConfigurationException(string? message = null)
        : base(message)
    {
    }
    public OptionsConfigurationException(string? message, Exception? inner = null) 
        : base(message, inner)
    {
    }
}