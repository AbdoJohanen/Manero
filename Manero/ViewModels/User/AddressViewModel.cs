using Manero.Models.Entities.UserEntities;
using System.ComponentModel.DataAnnotations;

namespace Manero.ViewModels.User;

public class AddressViewModel
{
    public string? UserId { get; set; }

    public int AddressId { get; set; }

    [Display(Name = "Title")]
    public string AddressTitle { get; set; } = null!;

    [Display(Name = "Street Name")]
    public string StreetName { get; set; } = null!;

    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City")]
    public string City { get; set; } = null!;

    public static implicit operator AddressEntity(AddressViewModel model)
    {
        return new AddressEntity
        {
            StreetName = model.StreetName,
            PostalCode = model.PostalCode,
            City = model.City
        };
    }

    public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();
}
