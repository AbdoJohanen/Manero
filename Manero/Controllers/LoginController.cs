using Manero.Helpers.Services.UserServices;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class LoginController : Controller
{
    private readonly AuthService _auth;

    public LoginController(AuthService auth)
    {
        _auth = auth;
    }

    public IActionResult Index(string ReturnUrl = null!)
    {
        ViewBag.ActivePage = "Login";

        var model = new UserLoginViewModel();
        if (ReturnUrl != null)
            model.ReturnUrl = ReturnUrl;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _auth.LoginAsync(model))
                return LocalRedirect(model.ReturnUrl);

            ModelState.AddModelError("", "Incorrect e-mail or password.");
        }

        return View(model);
    }
}
