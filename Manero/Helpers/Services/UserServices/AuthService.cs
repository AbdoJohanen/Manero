using Manero.Models.Identity;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Manero.Helpers.Services.UserServices;

public class AuthService
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

    public async Task<bool> ExistUserAsync(Expression<Func<AppUser, bool>> expression)
    {
        return await _userManager.Users.AnyAsync(expression);
    }

    public async Task<AppUser> RegisterAsync(UserRegisterViewModel model)
    {
        try
        {
            await _seedService.SeedRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            AppUser appUser = model;

            var result = await _userManager.CreateAsync(appUser, model.Password);

            await _userManager.AddToRoleAsync(appUser, roleName);

            return null!;
        }
        catch { return null!; }
    }

    public async Task<bool> LoginAsync(UserLoginViewModel model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return _signInManager.IsSignedIn(user);
    }
}
