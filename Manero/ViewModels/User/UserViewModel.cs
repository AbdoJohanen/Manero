namespace Manero.ViewModels.User;

public class UserViewModel
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Company { get; set; }
    public string Role { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public IEnumerable<AddressViewModel> Addresses { get; set; } = null!;
    public IList<string> Roles { get; set; } = null!;
}
