using Manero.Enums;
using Manero.Models.Identity;
using Manero.Models.Test;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Manero.Helpers.Services.UserServices;

public interface IAuthService
{
    Task<ServiceResponse<bool>> ExistUserAsync(Expression<Func<AppUser, bool>> expression);
    Task<ServiceResponse<AppUser>> RegisterAsync(ServiceRequest<UserRegisterViewModel> request);
    Task<ServiceResponse<bool>> LoginAsync(ServiceRequest<UserLoginViewModel> request);
    Task<ServiceResponse<bool>> LogoutAsync(ClaimsPrincipal user);
}

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AddressService _addressService;
    private readonly SeedService _seedService;

    public AuthService(UserManager<AppUser> userManager, AddressService addressService, SignInManager<AppUser> signInManager, SeedService seedService)
    {
        _userManager = userManager;
        _addressService = addressService;
        _signInManager = signInManager;
        _seedService = seedService;
    }

    public async Task<ServiceResponse<bool>> ExistUserAsync(Expression<Func<AppUser, bool>> expression)
    {
        var result = await _userManager.Users.AnyAsync(expression);
        return new ServiceResponse<bool>
        {
            Data = result,
            Status = StatusCode.Success,
        };
    }

    public async Task<ServiceResponse<AppUser>> RegisterAsync(ServiceRequest<UserRegisterViewModel> request)
    {
        try
        {
            var model = request.Data;
            await _seedService.SeedRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            AppUser appUser = model!;

            var result = await _userManager.CreateAsync(appUser, model!.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, roleName);
                return new ServiceResponse<AppUser>
                {
                    Data = appUser,
                    Success = true, // Modify based on the specific condition of success
                    Message = "User registered successfully" // Optional message
                };
            }
            else
            {
                // Handle the failure scenario if needed
                return new ServiceResponse<AppUser>
                {
                    Data = null,
                    Success = false, // Modify based on the specific condition of failure
                    Message = "User registration failed" // Optional message
                };
            }
        }
        catch
        {
            return new ServiceResponse<AppUser>
            {
                Data = null,
                Success = false, // Modify based on the specific condition of failure
                Message = "An error occurred during user registration" // Optional message
            };
        }
    }

    public async Task<ServiceResponse<bool>> LoginAsync(ServiceRequest<UserLoginViewModel> request)
    {
        var model = request.Data;
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model!.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model!.Password, model.RememberMe, false);
            return new ServiceResponse<bool>
            {
                Data = result.Succeeded,
                Status = StatusCode.Success,
                Success = true, // Modify based on the specific condition of success
                Message = result.Succeeded ? "User logged in successfully" : "Invalid login attempt" // Optional message
            };
        }

        return new ServiceResponse<bool>
        {
            Data = false,
            Status = StatusCode.NotFound,
            Success = false, // Modify based on the specific condition of failure
            Message = "User not found" // Optional message
        };
    }

    public async Task<ServiceResponse<bool>> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return new ServiceResponse<bool>
        {
            Data = !_signInManager.IsSignedIn(user),
            Success = true, // Modify based on the specific condition of success
            Message = "User logged out successfully" // Optional message
        };
    }
}
