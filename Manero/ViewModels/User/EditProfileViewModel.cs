using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Manero.ViewModels.User;

public class EditProfileViewModel
{
    public string? Id { get; set; }

    [Display(Name = "Name")]
    //[Required(ErrorMessage = "You must enter a name.")]
    public string? Name { get; set; } = null!;

    [Display(Name = "Street Name")]
    //[Required(ErrorMessage = "You must enter a street name.")]
    public string? StreetName { get; set; }

    [Display(Name = "Postal Code")]
    //[Required(ErrorMessage = "You must enter a postal code.")]
    public string? PostalCode { get; set; }

    [Display(Name = "City")]
    //[Required(ErrorMessage = "You must enter a city.")]
    public string? City { get; set; }

    [Display(Name = "PhoneNumber")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "PhoneNumberConfirmed")]
    public bool PhoneNumberConfirmed { get; set; } = false;

    [Display(Name = "E-mail Address")]
    //[Required(ErrorMessage = "You must enter an e-mail address.")]
    [RegularExpression(@"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your must enter a valid e-mail address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Profile Image")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Upload Profile Image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProfilePicture { get; set; }

    public static implicit operator AppUser(EditProfileViewModel model)
    {
        var appUser = new AppUser
        {
            UserName = model.Email,
            Name = model.Name!,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };

        if (model.ProfilePicture != null)
            appUser.ImageUrl = $"{appUser.Id}_{Path.GetFileName(model.ProfilePicture.FileName).Replace(" ", "_")}";

        else
            appUser.ImageUrl = $"no-thumb.jpg";

        return appUser;
    }

    public static implicit operator AddressEntity(EditProfileViewModel model)
    {
        return new AddressEntity
        {
            StreetName = model.StreetName!,
            PostalCode = model.PostalCode!,
            City = model.City!
        };
    }

    public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();

}
