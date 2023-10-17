using Manero.Models.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Manero.Models.Identity;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();
}
