using Manero.Helpers.Repositories.UserRepositories;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;
using Manero.ViewModels.User;
using System.Linq.Expressions;

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

    public async Task AddAddressAsync(AppUser user, AddressEntity address, AddressViewModel model)
    {
        await _userAddressRepo.AddAsync(new UserAddressEntity
        {
            UserId = user.Id,
            AddressId = address.Id,
            AddressTitle = model.AddressTitle
        });
    }

    public async Task<IEnumerable<UserAddressEntity>> GetAllUserAddressesAsync(Expression<Func<UserAddressEntity, bool>> userAddressExpression)
    {
        var userAddresses = await _userAddressRepo.GetAllAsync(userAddressExpression);

        // Extract AddressIds from UserAddressEntities
        var addressIds = userAddresses.Select(x => x.AddressId).ToList();

        // Create an expression to match AddressEntities with matching AddressIds
        Expression<Func<AddressEntity, bool>> addressExpression = x => addressIds.Contains(x.Id);

        // Retrieve all AddressEntities matching the expression
        var addresses = await _addressRepo.GetAllAsync(addressExpression);

        // Associate each UserAddressEntity with its AddressEntity
        foreach (var userAddress in userAddresses)
        {
            userAddress.Address = addresses.FirstOrDefault(x => x.Id == userAddress.AddressId)!;
        }

        return userAddresses;
    }

    public async Task<UserAddressEntity> GetUserAddresEntityBydIdAsync(int addressId)
    {
        // Loads the AddressEntity to be read by the _userAddressRepo.GetAsync
        AddressEntity adressEntity = await _addressRepo.GetAsync(x => x.Id == addressId);
        UserAddressEntity userAddressEntity = await _userAddressRepo.GetAsync(x => x.AddressId == addressId);

        return userAddressEntity;
    }

    public async Task<bool> DeleteUserAddressEntityAsync(UserAddressEntity userAddressEntity)
    {
        await _userAddressRepo.DeleteAsync(userAddressEntity);

        return true;
    }

    public async Task<UserAddressEntity> UpdateUserAddressEntityAsync(UserAddressEntity userAddressEntity)
    {
        await _userAddressRepo.UpdateAsync(userAddressEntity);

        return userAddressEntity;
    }
}
