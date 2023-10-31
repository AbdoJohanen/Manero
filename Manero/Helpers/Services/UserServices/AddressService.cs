using Manero.Helpers.Repositories.UserRepositories;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;

namespace Manero.Helpers.Services.UserServices;

public class AddressService
{
    private readonly AddressRepository _addressRepo;
    private readonly UserAddressRepository _userAddressRepo;

    public AddressService(AddressRepository addressRepo, UserAddressRepository userAddressRepo)
    {
        _addressRepo = addressRepo;
        _userAddressRepo = userAddressRepo;
    }

    public async Task<AddressEntity> GetorCreateAsync(AddressEntity address)
    {
        var entity = await _addressRepo.GetAsync(x =>
            x.StreetName == address.StreetName &&
            x.PostalCode == address.PostalCode &&
            x.City == address.City
        );

        entity ??= await _addressRepo.AddAsync(address);
        return entity!;
    }

    public async Task AddAddressAsync(AppUser user, AddressEntity address)
    {
        await _userAddressRepo.AddAsync(new UserAddressEntity
        {
            UserId = user.Id,
            AddressId = address.Id
        });
    }
}
