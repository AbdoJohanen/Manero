using Manero.Contexts;
using Manero.Models.Identity;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Manero.Helpers.Services.UserServices;

public class UserService
{
    private readonly UserRepository _userRepo;
    private readonly UserAddressRepository _userAddressRepo;
    private readonly AddressRepository _addressRepo;
    private readonly IdentityContext _identityContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserService(UserRepository userRepo, UserAddressRepository userAddressRepo, IdentityContext identityContext, AddressRepository addressRepo, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
    {
        _userRepo = userRepo;
        _userAddressRepo = userAddressRepo;
        _addressRepo = addressRepo;
        _identityContext = identityContext;
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<AppUser> AddUserAsync(AppUser user)
    {
        return await _userRepo.AddAsync(user);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
    {
        var profiles = new List<UserViewModel>();

        var users = await _userRepo.GetAllAsync();

        foreach (var user in users)
        {
            var addresses = await _identityContext.AspNetUsersAddresses.Where(x => x.UserId == user.Id).Select(x => x.Address).ToListAsync();
            var role = await _userManager.GetRolesAsync(user);

            var userViewModel = new UserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Name = user.Name,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Role = string.Join(",", role),
                ImageUrl = user.ImageUrl,
                Addresses = addresses.Select(a => new AddressViewModel
                {
                    StreetName = a.StreetName,
                    PostalCode = a.PostalCode,
                    City = a.City
                })
            };

            profiles.Add(userViewModel);

        }

        return profiles;
    }


    //public async Task<List<AppUser>> GetAllUsersAsync()
    //{
    //    return (List<AppUser>)await _userRepo.GetAllAsync();
    //}

    public async Task<AppUser> GetUserAsync(string userId)
    {
        return await _userRepo.GetAsync(u => u.Id == userId);
    }

    public async Task<bool> DeleteUserAsync(AppUser user)
    {
        return await _userRepo.DeleteAsync(user);
    }

    public async Task<AppUser> UpdateUserAsync(AppUser user)
    {
        return await _userRepo.UpdateAsync(user);
    }

    public async Task<string> GetRolesAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count > 0)
        {
            return roles[0]; // Assuming the user has a single role
        }

        return null!;
    }

    public async Task<AppUser> UploadImageAsync(AppUser user, IFormFile? image)
    {
        try
        {
            string imagePath = $"{_webHostEnvironment.WebRootPath}/assets/images/users/{user.Id}_{Path.GetFileName(image!.FileName).Replace(" ", "_")}";

            using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);
            return user;
        }
        catch { return null!; }
    }
}