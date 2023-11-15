using Manero.Helpers.Services.UserServices;
using Manero.Models.Identity;
using Manero.Models.Test;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Manero.Controllers;

public class RegisterController : Controller
{
    private readonly IAuthService _auth;
    private readonly IUserService _userService;
    private readonly SignInManager<AppUser> _signInManager;

    public RegisterController(IAuthService auth, IUserService userService, SignInManager<AppUser> signInManager)
    {
        _auth = auth;
        _userService = userService;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        ViewBag.ActivePage = "Register";

        if (_signInManager.IsSignedIn(User))
        {
            // If the user is signed in, redirect to the account page
            return RedirectToAction("index", "account");
        }
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
        try
        {
            if (ModelState.IsValid)
            {
                var userExists = await _auth.ExistUserAsync(x => x.Email == model.Email);  // Check if the user exists
                if (userExists.Data)
                {
                    ModelState.AddModelError("", "Email is already registered.");
                    return Conflict();
                    /*return View(model);*/ // Return the view with the error message
                }

                var request = new ServiceRequest<UserRegisterViewModel> { Data = model };
                var response = await _auth.RegisterAsync(request); // If user doesn't exist, save in database

                return RedirectToAction("done", "register", StatusCode((int)response.Status, response.Message));
            }
            else
            {
                return View(model);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
        }
    }
}
