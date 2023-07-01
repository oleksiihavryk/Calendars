using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Calendars.Authentication.ViewModels;
/// <summary>
///     View model for registration form.
/// </summary>
public class RegisterViewModel
{
    // Visible part of data for user.
    [Required] public string Name { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress),
     Display(Name = "Email (optional)")]
    public string? Email { get; set; } = null;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), Display(Name = "Password confirmation")] 
    public string PasswordConfirmation { get; set; } = string.Empty;

    // Invisible data for user.
    [Required] public string ReturnUrl { get; set; } = string.Empty;
    [Required] public string CancelUrl { get; set; } = string.Empty;
}