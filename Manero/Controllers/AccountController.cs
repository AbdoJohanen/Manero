using Manero.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Chat.V2;
using Manero.Models.DTO;
using Manero.Helpers.Services.UserServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Manero.Migrations;
using Microsoft.AspNetCore.Identity;
using Manero.Models.Identity;
using System.Security.Claims;
using Twilio.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Manero.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly AuthService _auth;
    private readonly UserManager<AppUser> _userManager;
    private readonly UserService _userService;

    public AccountController(AuthService auth, UserManager<AppUser> userManager, UserService userService)
    {
        _auth = auth;
        _userManager = userManager;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ActivePage = "Account";

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Find user's id
        var user = await _userService.GetUserAsync(userId!); // Get user details
        var profile = new Profile   // Create user profile 
        {
            Name = user!.Name,
            Email = user!.Email,
            ImageUrl = user!.ImageUrl,
            Addresses = user!.Addresses,
        };

        return View(profile);
    }

    public async Task<IActionResult> LogOut()
    {
        if (await _auth.LogoutAsync(User))
            return LocalRedirect("/");

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Profile profile)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = profile.PhoneNumber;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Find user's id
        var user = await _userService.GetUserAsync(userId!); // Get user details
        var userModel = new EditProfileViewModel   // Create a user model and write all new details to input
        {
            Id = user.Id,
            Name = user!.Name,
            Email = user!.Email!,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            ImageUrl = user!.ImageUrl,
            Addresses = user!.Addresses,
        };

        return View(userModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        ViewBag.ActivePage = "Account";

        if (ModelState.IsValid)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userService.GetUserAsync(userId!);

                // Update the user's properties with the values from the model
                user.UserName = model.Email;
                user.Name = model.Name!;
                user.Email = model.Email!;                 
                if (model.PhoneNumber != user.PhoneNumber)
                {
                    user.PhoneNumberConfirmed = false;
                }
                else { }
                user.PhoneNumber = model.PhoneNumber!;

                if (model.ProfilePicture  != null) 
                {
                    await _userService.UploadImageAsync(user, model.ProfilePicture!);
                    user.ImageUrl = $"{user.Id}_{Path.GetFileName(model.ProfilePicture.FileName).Replace(" ", "_")}";
                }
                else { }                

                await _userService.UpdateUserAsync(user); // Update the user in the database

                return RedirectToAction("edit", "account");
            }
            catch { }

            return RedirectToAction("index", "account");
        }

        return View(model);
    }

    public IActionResult PhoneNumber(string phoneNumber)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = phoneNumber;
        return View();
    }

    [HttpPost]
    public ActionResult PhoneNumber(EditProfileViewModel model)
    {
        const string accountSid = "AC4660a240758971694515f811d31f7bde";
        const string authToken = "fc8a9c90cbe6211c68777f6b257b5ca8";
        const string serviceSid = "VAfe2ec4404a8b83d31a4a7dc5936a66f8";

        TwilioClient.Init(accountSid, authToken);

        var verification = VerificationResource.Create(
            to: model.PhoneNumber,
            channel: "sms",
            pathServiceSid: serviceSid
        );

        if (verification != null)
        {
            return RedirectToAction("verify", "account", new { phoneNumber = model.PhoneNumber });
        }

        return RedirectToAction("phonenumber");
    }

    [HttpGet]
    public IActionResult Verify(string phoneNumber)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = phoneNumber;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Verify(EditProfileViewModel model, string[] code)
    {
        string combinedCode = string.Join("", code);

        const string accountSid = "AC4660a240758971694515f811d31f7bde";
        const string authToken = "fc8a9c90cbe6211c68777f6b257b5ca8";
        const string serviceSid = "VAfe2ec4404a8b83d31a4a7dc5936a66f8";

        TwilioClient.Init(accountSid, authToken);

        var verificationCheck = VerificationCheckResource.Create(
            to: model.PhoneNumber,
            code: combinedCode,
            pathServiceSid: serviceSid
        );

        if (verificationCheck.Status == "approved")
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userService.GetUserAsync(userId!);

                if (user != null)
                {
                    // Update the PhoneNumberConfirmed property and other relevant properties
                    user.PhoneNumberConfirmed = true;

                    // Call the UpdateUserAsync method to update the user in the database
                    await _userService.UpdateUserAsync(user);
                }
                else
                {
                    // Handle the case when the user is not found
                    return RedirectToAction("index", "account");
                }
            }
            catch { }

            return RedirectToAction("edit", new { phoneNumber = model.PhoneNumber });
        }
        else
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Resend(string phoneNumber)
    {
        try
        {
            const string accountSid = "AC4660a240758971694515f811d31f7bde";
            const string authToken = "fc8a9c90cbe6211c68777f6b257b5ca8";
            const string serviceSid = "VAfe2ec4404a8b83d31a4a7dc5936a66f8";

            TwilioClient.Init(accountSid, authToken);

            var verification = await VerificationResource.CreateAsync(
                to: phoneNumber,
                channel: "sms",
                pathServiceSid: serviceSid
            );

            if (verification != null && verification.Status == "pending")
            {
                return RedirectToAction("verify", "account", new { phoneNumber = phoneNumber });
            }
        }
        catch { }

        return RedirectToAction("phonenumber");
    }

    public IActionResult Address()
    {
        ViewBag.ActivePage = "Account";
        return View();
    }
}
