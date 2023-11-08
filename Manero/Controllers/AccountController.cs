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
using MimeKit;
using MailKit.Net.Smtp;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;
using Manero.Models.Test;

namespace Manero.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IAuthService _auth;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;

    public AccountController(IAuthService auth, UserManager<AppUser> userManager, IUserService userService)
    {
        _auth = auth;
        _userManager = userManager;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ActivePage = "Account";

        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Find user's id
            var response = await _userService.GetAsync(userId!); // Get user details
            if (response.Success)
            {
                var user = response.Data;
                var profile = new Profile // Create user profile
                {
                    Name = user!.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    Addresses = user.Addresses,
                };
                return View(profile);
            }
            else
            {
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            // Handle the exception if needed
            Debug.WriteLine(ex.Message);
            return View("Error");
        }
    }

    public async Task<IActionResult> LogOut()
    {
        var result = await _auth.LogoutAsync(User);
        if (result.Data)
        {
            return LocalRedirect("/");
        }

        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult ForgotPassword(string email)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.Email = email;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(EditProfileViewModel model)
    {
        ViewBag.ActivePage = "Account";

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
           
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user!);

                SendResetPasswordLink(model.Email, token);
                ViewBag.Email = model.Email; // Pass the email to the view
            }
            else
            {
                ModelState.AddModelError("", "We couldn't find a registered email as you wrote. :(");
            }
        }
        else
        {
            ViewBag.Email = null; // Reset the email value
        }

        return View();
    }

    private void SendResetPasswordLink(string email, string token)
    {
        var callbackUrl = Url.Action("resetpassword", "account", new { email, token }, protocol: Request.Scheme);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Grupp Projekt", "Javadov@hotmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Reset Password";

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
        <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
        <html data-editor-version='2' class='sg-campaigns' xmlns='http://www.w3.org/1999/xhtml'>
        <head>
            <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>
            <meta http-equiv='X-UA-Compatible' content='IE=Edge'>
            <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx' crossorigin='anonymous'>
        </head>
            <body>
                <style type='text/css'>
                    @import url('https://fonts.googleapis.com/css2?family=Mulish:wght@400;600;700&family=Pacifico&display=swap');        
                body {{
	                 background-color: #f4f0f0;
                }}
                 body .container {{
	                 width: 500px;
	                 display: flex;
	                 flex-direction: column;
	                 justify-content: center;
	                 align-items: center;
	                 text-align: center;
	                 padding-top: 20px;
                }}
                 body .container .content {{
	                 background-color: #f9f6f6;
	                 margin-top: 20px;
	                 padding: 50px;
                }}
                 body .container .content h3 {{
	                 font-family: 'Mulish', sans-serif;
	                 font-family: 'Pacifico', cursive;
                }}
                 body .container .content p {{
	                 font-family: 'Mulish', sans-serif;
                }}
                 body .container .content span {{
	                 font-family: 'Mulish', sans-serif;
                }}
                 body .container .content a {{
	                 width: 150px;
	                 font-family: 'Mulish', sans-serif;
	                 background-color: rgba(243, 240, 63, 0.916);
                }}       
            </style>    
                <div class='container '>
                    <img src='https://i.ibb.co/Y27xpJj/logo.png' alt='manero' />
                    <div class='content'>
                        <h3>Reset password</h3>
                        <br>
                        <p>Let's reset your password so you can get back to shopping.</p>
                        <span>Thank you!</span>
                        <br>
                        <br>
                        <a type='button' href='{callbackUrl}' class='btn btn-warning'>Reset Password</a>
                    </div> 
                </div>
            </body>
        </html>";

        // Attach the HTML body to the message
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.sendgrid.net", 587, false);
            client.Authenticate("apikey", "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");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> ResetPassword(string email, string token)
    {
        // Retrieve the user by email
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && token != null)
        {
            return View();
        }
        else
        {
            ModelState.AddModelError("", "Sorry, Something is wrong.");
            return RedirectToAction("index", "login");
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.ResetPassword = true;
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "We couldn't find a registered user. :(");
            }
        }
        else { }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Profile profile)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = profile.PhoneNumber;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Find user's id
        var response = await _userService.GetAsync(userId!);

        if (response.Success)
        {
            var user = response.Data;
            var userModel = new EditProfileViewModel   // Create a user model and write all new details to input
            {
                Id = user!.Id,
                Name = user!.Name,
                Email = user.Email!,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                ImageUrl = user.ImageUrl,
                Addresses = user.Addresses,
            };

            return View(userModel);
        }
        else
        {
            return RedirectToAction("Error");
        }
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
                var response = await _userService.GetAsync(userId!);

                if (response.Success)
                {
                    var user = response.Data;

                    // Update the user's properties with the values from the model
                    user!.UserName = model.Email;
                    user.Name = model.Name!;
                    if (model.Email != user.Email)
                    {
                        user.EmailConfirmed = false;
                    }
                    user.Email = model.Email!;
                    if (model.PhoneNumber != user.PhoneNumber)
                    {
                        user.PhoneNumberConfirmed = false;
                    }
                    user.PhoneNumber = model.PhoneNumber!;
                    if (model.ProfilePicture != null)
                    {
                        await _userService.UploadImageAsync(user, model.ProfilePicture);
                        user.ImageUrl = $"{user.Id}_{Path.GetFileName(model.ProfilePicture.FileName).Replace(" ", "_")}";
                    }

                    var updateResponse = await _userService.UpdateAsync(new ServiceRequest<AppUser> { Data = user }); // Update the user in the database

                    if (updateResponse.Success)
                    {
                        return RedirectToAction("edit", "account");
                    }
                    else
                    {
                        return RedirectToAction("index", "account");
                    }
                }
                else
                {
                    return RedirectToAction("index", "account");
                }
            }
            catch
            {
                return RedirectToAction("index", "account");
            }
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
    public IActionResult PhoneNumber(EditProfileViewModel model)
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
            return RedirectToAction("verifyphonenumber", "account", new { phoneNumber = model.PhoneNumber });
        }

        return RedirectToAction("phonenumber");
    }

    [HttpGet]
    public IActionResult VerifyPhoneNumber(string phoneNumber)
    {
        ViewBag.ActivePage = "Account";
        ViewBag.PhoneNumber = phoneNumber;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyPhoneNumber(EditProfileViewModel model, string[] code)
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

                var response = await _userService.GetAsync(userId!);

                if (response.Success && response.Data != null)
                {
                    var user = response.Data;
                    // Update the PhoneNumberConfirmed property and other relevant properties
                    user.PhoneNumberConfirmed = true;

                    // Call the UpdateAsync method to update the user in the database
                    var updateResponse = await _userService.UpdateAsync(new ServiceRequest<AppUser> { Data = user });

                    if (!updateResponse.Success)
                    {
                        return RedirectToAction("edit", "account");
                    }
                }
                else
                {
                    return RedirectToAction("index", "account");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("edit", new { phoneNumber = model.PhoneNumber });
        }
        else
        {
            return View();
        }
    }

    public IActionResult Resend(string phoneNumber)
    {
        if (phoneNumber != null)
        {
            ResendVerification(phoneNumber);
            return View("verifyphonenumber", "account");
        }
        else
        {
            return View("phonenumber", "account");
        }
    }

    private void ResendVerification(string phoneNumber)
    {
        try
        {
            const string accountSid = "AC4660a240758971694515f811d31f7bde";
            const string authToken = "fc8a9c90cbe6211c68777f6b257b5ca8";
            const string serviceSid = "VAfe2ec4404a8b83d31a4a7dc5936a66f8";

            TwilioClient.Init(accountSid, authToken);

            var verification = VerificationResource.CreateAsync(
                to: phoneNumber,
                channel: "sms",
                pathServiceSid: serviceSid
            );
        }
        catch { }
    }

    public async Task <ActionResult> VerifyEmail( string email)
    {
        // Call the SendVerificationEmail function
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);

            SendVerificationLink(user!, email, token);
            ViewBag.Email = email;
        }
        else 
        {
            ModelState.AddModelError("", "We couldn't find a registered email as you wrote. :(");
        }

        return View();
    }

    private void SendVerificationLink(AppUser user, string email, string token)
    {
        var callbackUrl = Url.Action("verification", "account", new { email, token }, protocol: Request.Scheme);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Grupp Projekt", "Javadov@hotmail.com"));
        message.To.Add(new MailboxAddress(user.Name, email));
        message.Subject = "Email Verification";

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
        <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
        <html data-editor-version='2' class='sg-campaigns' xmlns='http://www.w3.org/1999/xhtml'>
        <head>
            <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>
            <meta http-equiv='X-UA-Compatible' content='IE=Edge'>
            <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx' crossorigin='anonymous'>
        </head>
            <body>
                <style type='text/css'>
                    @import url('https://fonts.googleapis.com/css2?family=Mulish:wght@400;600;700&family=Pacifico&display=swap');        
                body {{
	                 background-color: #f4f0f0;
                }}
                 body .container {{
	                 width: 500px;
	                 display: flex;
	                 flex-direction: column;
	                 justify-content: center;
	                 align-items: center;
	                 text-align: center;
	                 padding-top: 20px;
                }}
                 body .container .content {{
	                 background-color: #f9f6f6;
	                 margin-top: 20px;
	                 padding: 50px;
                }}
                 body .container .content h3 {{
	                 font-family: 'Mulish', sans-serif;
	                 font-family: 'Pacifico', cursive;
                }}
                 body .container .content p {{
	                 font-family: 'Mulish', sans-serif;
                }}
                 body .container .content span {{
	                 font-family: 'Mulish', sans-serif;
                }}
                 body .container .content a {{
	                 width: 150px;
	                 font-family: 'Mulish', sans-serif;
	                 background-color: rgba(243, 240, 63, 0.916);
                }}       
            </style>    
                <div class='container '>
                    <img src='https://i.ibb.co/Y27xpJj/logo.png' alt='manero' />
                    <div class='content'>
                        <h3>Thanks for signing up, </h3>
                        <h3><b>{user.Name}</b></h3>
                        <br>
                        <p>Please verify your email address to get access.</p>
                        <span>Thank you!</span>
                        <br>
                        <br>
                        <a type='button' href='{callbackUrl}' class='btn btn-warning'>Verify Email</a>
                    </div> 
                </div>
            </body>
        </html>";

        // Attach the HTML body to the message
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.sendgrid.net", 587, false);
            client.Authenticate("apikey", "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");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Verification(string email, string token)
    {
        // Retrieve the user by email
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && token != null)
        {
            // Update the EmailConfirmed property to true
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            // Redirect to a page indicating successful email verification
            return RedirectToAction("edit", "account");
        }
        else
        {
            // Redirect to a page indicating failed email verification
            return RedirectToAction("index", "account");
        }
    }

    public IActionResult Address()
    {
        ViewBag.ActivePage = "Account";
        return View();
    }
}