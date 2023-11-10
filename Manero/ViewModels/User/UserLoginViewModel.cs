using Manero.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Manero.ViewModels.User;

public class UserLoginViewModel
{
    [Display(Name = "E-mail Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Keep me logged in")]
    public bool RememberMe { get; set; } = false;

    public string ReturnUrl { get; set; } = "/account";

    public static implicit operator AppUser(UserLoginViewModel model)
    {
        return new AppUser
        {
            UserName = model.Email,
            Email = model.Email
        };
    }
}
