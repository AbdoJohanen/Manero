using Manero.Helpers.Services.UserServices;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class RegisterController : Controller
{
    private readonly AuthService _auth;
    private readonly UserService _userService;

    public RegisterController(AuthService auth, UserService userService)
    {
        _auth = auth;
        _userService = userService;
    }

    public IActionResult Index()
    {
        ViewBag.ActivePage = "Register";
        return View();
    }

    public IActionResult Done()
    {
        ViewBag.ActivePage = "Register";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userExists = await _auth.ExistUserAsync(x => x.Email == model.Email);  // Check user if exist or not
            if (userExists)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View(model); // Return the view with the error message
            }

            var user = await _auth.RegisterAsync(model); // If user doesn't exist, save in database
            return RedirectToAction("done", "register");
        }

        return View(model);
    }
}
