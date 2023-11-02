using Manero.Helpers.Services.UserServices;
using Manero.Models.Identity;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class LoginController : Controller
{
    private readonly AuthService _auth;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginController(AuthService auth, SignInManager<AppUser> signInManager)
    {
        _auth = auth;
        _signInManager = signInManager;
    }

    public IActionResult Index(string ReturnUrl = null!)
    {
        ViewBag.ActivePage = "Login";

        var model = new UserLoginViewModel();
        if (ReturnUrl != null)
            model.ReturnUrl = ReturnUrl;

        if (_signInManager.IsSignedIn(User))
        {
            // If the user is signed in, redirect to the account page
            return RedirectToAction("index", "account");
        }

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
