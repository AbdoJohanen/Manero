using Manero.Contexts;
using Manero.Models.Identity;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Manero.Models.Test;
using Manero.Migrations;
using Manero.Models.DTO;
using System.Linq.Expressions;
using System.Diagnostics;
using Manero.Enums;

namespace Manero.Helpers.Services.UserServices;

public interface IUserService
{
    Task<ServiceResponse<AppUser>> AddUserAsync(ServiceRequest<AppUser> request);
    Task<ServiceResponse<IEnumerable<AppUser>>> GetAllUsersAsync();
    Task<ServiceResponse<AppUser>> GetAsync(string request);
    Task<ServiceResponse<AppUser>> UpdateAsync(ServiceRequest<AppUser> request);
    Task<bool> DeleteAsync(ServiceRequest<AppUser> request);
    Task<AppUser> UploadImageAsync(AppUser user, IFormFile? image);
}

public class UserService : IUserService
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

    public async Task<ServiceResponse<AppUser>> AddUserAsync(ServiceRequest<AppUser> request)
    {
        var response = new ServiceResponse<AppUser>();

        try
        {
            if (request.Data != null)
            {
                response.Data = await _userRepo.AddAsync(request.Data!);
                response.Status = StatusCode.Created;
            }
            else
            {
                response.Data = null;
                response.Status = StatusCode.Conflict;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Data = null;
            response.Status = StatusCode.InternalServerError;
        }
        return response;
    }

    public async Task<ServiceResponse<IEnumerable<AppUser>>> GetAllUsersAsync()
    {
        var response = new ServiceResponse<IEnumerable<AppUser>>();

        try
        {
            var users = await _userRepo.GetAllAsync();
            response.Data = users;
            response.Success = true;
            response.Message = "Retrieved all users successfully.";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Data = null;
            response.Success = false;
            response.Message = "An error occurred while retrieving all users.";
        }
        return response;
    }

    public async Task<ServiceResponse<AppUser>> GetAsync(string userId)
    {
        var response = new ServiceResponse<AppUser>();

        try
        {
            var user = await _userRepo.GetAsync(u => u.Id == userId);
            if (user != null)
            {
                response.Data = user;
                response.Success = true;
                response.Message = "User retrieved successfully.";
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = "User not found.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Data = null;
            response.Success = false;
            response.Message = "An error occurred while retrieving the user.";
        }
        return response;
    }

    //public async Task<bool> DeleteUserAsync(AppUser user)
    //{
    //    return await _userRepo.DeleteAsync(user);
    //}

    public async Task<bool> DeleteAsync(ServiceRequest<AppUser> request)
    {
        try
        {
            if (request.Data != null)
            {
                return await _userRepo.DeleteAsync(request.Data);
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    //public async Task<AppUser> UpdateUserAsync(AppUser user)
    //{
    //    return await _userRepo.UpdateAsync(user);
    //}

    public async Task<ServiceResponse<AppUser>> UpdateAsync(ServiceRequest<AppUser> request)
    {
        var response = new ServiceResponse<AppUser>();

        try
        {
            if (request.Data != null)
            {
                response.Data = await _userRepo.UpdateAsync(request.Data);
                response.Success = true;
                response.Message = "User updated successfully.";
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = "Failed to update user. Data is null.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Data = null;
            response.Success = false;
            response.Message = "An error occurred while updating the user.";
        }
        return response;
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