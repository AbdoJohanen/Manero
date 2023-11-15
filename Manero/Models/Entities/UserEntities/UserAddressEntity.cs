using Manero.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero.Models.Entities.UserEntities;

[PrimaryKey(nameof(UserId), nameof(AddressId))]
public class UserAddressEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public string AddressTitle { get; set; } = null!;
    public string AddressIcon { get; set; } = null!;
    public bool PrimaryAddress { get; set; } = false;
}
