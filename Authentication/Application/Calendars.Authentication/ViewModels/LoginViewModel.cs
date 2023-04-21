using System.ComponentModel.DataAnnotations;

namespace Calendars.Authentication.ViewModels;
/// <summary>
///     View model for login form.
/// </summary>
public class LoginViewModel
{
    // Visible part of data for user.
    [Required, Display(Name = "Name or Email")] public string Login { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty; 

    // Invisible data for user
    [Required] public string ReturnUrl { get; set; } = string.Empty;
}