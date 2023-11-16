using Manero.Helpers.Repositories.UserRepositories;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;
using Manero.Models.Test;
using Manero.ViewModels.User;
using System.Linq.Expressions;

namespace Manero.Helpers.Services.UserServices;

public interface IAddressService
{
    Task<ServiceResponse<AddressEntity>> GetorCreateAsync(ServiceRequest<AddressEntity> request);
    Task<ServiceResponse<bool>> AddAddressAsync(ServiceRequest<(AppUser user, AddressEntity address, AddressViewModel model)> request);
    Task<ServiceResponse<IEnumerable<UserAddressEntity>>> GetAllUserAddressesAsync(ServiceRequest<Expression<Func<UserAddressEntity, bool>>> request);
    Task<ServiceResponse<UserAddressEntity>> GetUserAddresEntityBydIdAsync(int request);
    Task<ServiceResponse<UserAddressEntity>> UpdateUserAddressEntityAsync(ServiceRequest<UserAddressEntity> request);
    Task<ServiceResponse<bool>> DeleteUserAddressEntityAsync(ServiceRequest<UserAddressEntity> request);
}

public class AddressService : IAddressService
{
    private readonly AddressRepository _addressRepo;
    private readonly UserAddressRepository _userAddressRepo;

    public AddressService(AddressRepository addressRepo, UserAddressRepository userAddressRepo)
    {
        _addressRepo = addressRepo;
        _userAddressRepo = userAddressRepo;
    }

    public async Task<ServiceResponse<AddressEntity>> GetorCreateAsync(ServiceRequest<AddressEntity> request)
    {
        try
        {
            var address = request.Data;

            var entity = await _addressRepo.GetAsync(x =>
                x.StreetName == address!.StreetName &&
                x.PostalCode == address.PostalCode &&
                x.City == address.City
            );

            entity ??= await _addressRepo.AddAsync(address!);

            return new ServiceResponse<AddressEntity>
            {
                Data = entity!,
                Success = true,
                Message = "Get or create operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            // Handle any exceptions here and return a failure response
            return new ServiceResponse<AddressEntity>
            {
                Data = null,
                Success = false,
                Message = $"Error during Get or Create operation: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<bool>> AddAddressAsync(ServiceRequest<(AppUser user, AddressEntity address, AddressViewModel model)> request)
    {
        try
        {
            var (user, address, model) = request.Data;

            await _userAddressRepo.AddAsync(new UserAddressEntity
            {
                UserId = user.Id,
                AddressId = address.Id,
                AddressTitle = model.AddressTitle
            });

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Add address operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            // Handle any exceptions here and return a failure response
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = $"Error during Add address operation: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<IEnumerable<UserAddressEntity>>> GetAllUserAddressesAsync(ServiceRequest<Expression<Func<UserAddressEntity, bool>>> request)
    {
        try
        {
            var userAddressExpression = request.Data;

            var userAddresses = await _userAddressRepo.GetAllAsync(userAddressExpression!);

            var addressIds = userAddresses.Select(x => x.AddressId).ToList();

            Expression<Func<AddressEntity, bool>> addressExpression = x => addressIds.Contains(x.Id);

            var addresses = await _addressRepo.GetAllAsync(addressExpression);

            foreach (var userAddress in userAddresses)
            {
                userAddress.Address = addresses.FirstOrDefault(x => x.Id == userAddress.AddressId)!;
            }

            return new ServiceResponse<IEnumerable<UserAddressEntity>>
            {
                Data = userAddresses,
                Success = true,
                Message = "Get all user addresses operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<UserAddressEntity>>
            {
                Data = null,
                Success = false,
                Message = $"Error during Get all user addresses operation: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<UserAddressEntity>> GetUserAddresEntityBydIdAsync(int request)
    {
        var response = new ServiceResponse<UserAddressEntity>();

        try
        {
            var addressId = request;

            AddressEntity addressEntity = await _addressRepo.GetAsync(x => x.Id == addressId);
            UserAddressEntity userAddressEntity = await _userAddressRepo.GetAsync(x => x.AddressId == addressId);

            return new ServiceResponse<UserAddressEntity>
            {
                Data = userAddressEntity,
                Success = true,
                Message = "Get user address entity by ID operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<UserAddressEntity>
            {
                Data = null,
                Success = false,
                Message = $"Error during Get user address entity by ID operation: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteUserAddressEntityAsync(ServiceRequest<UserAddressEntity> request)
    {
        try
        {
            var userAddressEntity = request.Data;

            await _userAddressRepo.DeleteAsync(userAddressEntity!);

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Delete user address entity operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            // Handle any exceptions here and return a failure response
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = $"Error during Delete user address entity operation: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<UserAddressEntity>> UpdateUserAddressEntityAsync(ServiceRequest<UserAddressEntity> request)
    {
        try
        {
            var userAddressEntity = request.Data;

            await _userAddressRepo.UpdateAsync(userAddressEntity!);

            return new ServiceResponse<UserAddressEntity>
            {
                Data = userAddressEntity,
                Success = true,
                Message = "Update user address entity operation completed successfully."
            };
        }
        catch (Exception ex)
        {
            // Handle any exceptions here and return a failure response
            return new ServiceResponse<UserAddressEntity>
            {
                Data = null,
                Success = false,
                Message = $"Error during Update user address entity operation: {ex.Message}"
            };
        }
    }
}
