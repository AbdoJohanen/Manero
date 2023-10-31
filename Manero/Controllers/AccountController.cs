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

namespace Manero.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly AuthService _auth;

    public AccountController(AuthService auth)
    {
        _auth = auth;
    }
    public IActionResult Index()
    {
        ViewBag.ActivePage = "Account";
        return View();
    }
    public async Task<IActionResult> LogOut()
    {
        if (await _auth.LogoutAsync(User))
            return LocalRedirect("/");

        return RedirectToAction("Index");
    }

    public IActionResult Edit(Profile user)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = user.PhoneNumber;
        return View();
    }

    [HttpPost]
    public IActionResult Edit(EditProfileViewModel model)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = model.PhoneNumber;
        return View();
    }

    public IActionResult PhoneNumber()
    {
        ViewBag.ActivePage = "Account";
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
            return RedirectToAction("verify", new { phoneNumber = model.PhoneNumber });
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
    public ActionResult Verify(EditProfileViewModel model, string[] code)
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
            return RedirectToAction("edit", new { phoneNumber = model.PhoneNumber });
        }
        else
        {
            return View();
        }
    }
}
