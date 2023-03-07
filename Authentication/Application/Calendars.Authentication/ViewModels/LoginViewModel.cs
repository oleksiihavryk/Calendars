using System.ComponentModel.DataAnnotations;

namespace Calendars.Authentication.ViewModels;
/// <summary>
///     View model for login form.
/// </summary>
public class LoginViewModel
{
    [Required] [Display(Name = "Name or Email")] public string Login { get; set; }
    [Required] [DataType(DataType.Password)] public string Password { get; set; }
    public string ReturnUrl { get; set; } = string.Empty;
}