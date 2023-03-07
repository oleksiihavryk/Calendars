using System.ComponentModel.DataAnnotations;

namespace Calendars.Authentication.ViewModels;
/// <summary>
///     View model for registration form.
/// </summary>
public class RegisterViewModel
{
    [Required] public string Name { get; set; }
    [DataType(DataType.EmailAddress)] public string? Email { get; set; }
    [Required] [DataType(DataType.Password)] public string Password { get; set; }
    [Required] [DataType(DataType.Password)] public string PasswordConfirmation { get; set; }
    public string ReturnUrl { get; set; } = string.Empty;

}