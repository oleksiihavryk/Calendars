using System.Security.Claims;
using Calendars.Authentication.Domain;
using Calendars.Authentication.Exceptions;
using Calendars.Authentication.Options;
using Calendars.Authentication.ViewModels;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Calendars.Authentication.Controllers;
/// <summary>
///     Controller for authentication operations.
/// </summary>
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly string _cancelUrl;

    public AccountController(
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        ILogger<AccountController> logger, 
        IIdentityServerInteractionService interactionService,
        IOptions<CancelUrlOptions> cancelUrl)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _interactionService = interactionService;
        _cancelUrl = cancelUrl.Value.Url;
    }

    [HttpGet] public async Task<ViewResult> Login([FromQuery] string returnUrl)
        => await Task.FromResult(View(new LoginViewModel()
        {
            ReturnUrl = returnUrl,
            CancelUrl = _cancelUrl
        }));
    [HttpGet] public async Task<ViewResult> Register([FromQuery] string returnUrl)
        => await Task.FromResult(View(new RegisterViewModel()
        {
            ReturnUrl = returnUrl,
            CancelUrl = _cancelUrl
        }));
    [HttpGet] public async Task<RedirectResult> Logout([FromQuery] string logoutId)
    {
        await _signInManager.SignOutAsync();
        var context = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(context.PostLogoutRedirectUri);
    }
    [HttpPost] public async Task<IActionResult> Login(
        [FromForm] LoginViewModel model)
    {
        if (ModelState.IsValid == false)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Login) ??
                   await _userManager.FindByNameAsync(model.Login);

        if (user == null)
            return UnknownLoginOrPassword(model);

        var result = await SignInUserAsync(user, model.Password);

        if (result.Succeeded == false)
            return UnknownLoginOrPassword(model);

        return Redirect(model.ReturnUrl);
    }
    [HttpPost] public async Task<IActionResult> Register(
        [FromForm] RegisterViewModel model)
    {
        try
        {
            if (ModelState.IsValid == false)
                return View(model);

            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError(
                    key: string.Empty,
                    errorMessage: "Password and password confirmation are not the same.");
                return View(model);
            }

            var user = new User(model.Name);
            if (string.IsNullOrWhiteSpace(model.Email) == false)
            {
                user.Email = model.Email;
                user.EmailConfirmed = true;
            };

            var userCreatingResult = await _userManager.CreateAsync(user, model.Password);

            if (userCreatingResult.Succeeded == false)
            {
                foreach (var error in userCreatingResult.Errors)
                    ModelState.AddModelError(
                        key: string.Empty,
                        errorMessage: error.Description);
                return View(model);
            }

            await RegisterClaimsAsync(user);

            var signInResult = await SignInUserAsync(user, model.Password);

            if (signInResult.Succeeded == false)
                throw new UserRegistrationException();

            return Redirect(model.ReturnUrl);
        }
        catch (UserRegistrationException ex)
        {
            _logger.LogError(exception: ex, message: ex.Message);
            throw;
        }
    }

    private ViewResult UnknownLoginOrPassword(LoginViewModel model)
    {
        ModelState.AddModelError(
            key: string.Empty,
            errorMessage: "Incorrect credentials. Wrong login or password.");
        return View(model);
    }
    private async Task RegisterClaimsAsync(User user)
    {
        await _userManager.AddClaimAsync(
            user,
            new Claim(JwtClaimTypes.Subject, user.Id));
        await _userManager.AddClaimAsync(
            user,
            new Claim(JwtClaimTypes.Name, user.UserName));
        if (user.Email != null)
        {
            await _userManager.AddClaimAsync(
                user,
                new Claim(JwtClaimTypes.Email, user.Email));
        }
    }
    private async Task<SignInResult> SignInUserAsync(User user, string password)
        => await _signInManager.PasswordSignInAsync(
            user,
            password,
            isPersistent: false,
            lockoutOnFailure: false);
}