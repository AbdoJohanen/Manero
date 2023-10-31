using Manero.Models.Entities.UserEntities;

namespace Manero.Models.DTO;

public class Profile
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();
}
