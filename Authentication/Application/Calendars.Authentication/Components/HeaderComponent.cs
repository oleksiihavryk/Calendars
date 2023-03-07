using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Calendars.Authentication.Components;
/// <summary>
///     Header component of all views.
/// </summary>
public class HeaderComponent : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync()
        => await Task.FromResult(View());
}