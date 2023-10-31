using Manero.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Manero.ViewModels.User;

public class UserRegisterViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "You must enter a name.")]
    public string Name { get; set; } = null!;

    [Display(Name = "E-mail Address")]
    [Required(ErrorMessage = "You must enter an e-mail address.")]
    [RegularExpression(@"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your must enter a valid e-mail address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "You must enter a password.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Your must enter a valid password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "You must confirm your password.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public static implicit operator AppUser(UserRegisterViewModel model)
    {
        var appUser = new AppUser
        {
            UserName = model.Email,
            Name = model.Name,
            Email = model.Email,
            ImageUrl = $"no-thumb.jpg",
        };

        return appUser;
    }
}
