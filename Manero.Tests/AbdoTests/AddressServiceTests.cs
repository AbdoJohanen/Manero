using Manero.Contexts;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Test;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.AbdoTests;

public class AddressServiceTests
{
    private readonly AddressService _addressService;
    private readonly IdentityContext _identityContext;
    private readonly AddressRepository _addressRepository;
    private readonly UserAddressRepository _userAddressRepository;


    public AddressServiceTests()
    {
        var options = new DbContextOptionsBuilder<IdentityContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

        _identityContext = new IdentityContext(options);
        _addressRepository = new AddressRepository(_identityContext);
        _userAddressRepository = new UserAddressRepository(_identityContext);
        _addressService = new AddressService(_addressRepository, _userAddressRepository);


    }

    [Fact]
    public async Task GetOrCreateAsync_Should_GetEntityOrCreateEntity_FromOrTo_Database()
    {
        // Arrange
        AddressEntity addressEntity = new()
        {
            Id = 1,
            StreetName = "Addressvägen 1",
            PostalCode = "12345",
            City = "Örebro"
        };

        // Act
        var request = new ServiceRequest<AddressEntity> { Data = addressEntity };

        // Act
        var result = await _addressService.GetorCreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ServiceResponse<AddressEntity>>(result);

        // Check the contents of the response data
        Assert.NotNull(result.Data);
        Assert.IsType<AddressEntity>(result.Data);
        Assert.Equal(addressEntity.Id, result.Data.Id);
        Assert.Equal(addressEntity.StreetName, result.Data.StreetName);
        Assert.Equal(addressEntity.PostalCode, result.Data.PostalCode);
        Assert.Equal(addressEntity.City, result.Data.City);

        await DisposeAsync();
    }


    [Fact]
    public async Task GetUserAddressEntityByIdAsync_Should_ReturnUserAddressEntity_When_AddressIdExists()
    {
        // Arrange
        UserAddressEntity userAddressEntity = new UserAddressEntity
        {
            UserId = "userId",
            AddressId = 1,
            AddressTitle = "Home"
        };

        await _userAddressRepository.AddAsync(userAddressEntity);

        // Act
        var result = await _addressService.GetUserAddresEntityBydIdAsync(userAddressEntity.AddressId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ServiceResponse<UserAddressEntity>>(result);
        Assert.Equal(userAddressEntity.UserId, result.Data!.UserId);
        Assert.Equal(userAddressEntity.AddressId, result.Data!.AddressId);
        Assert.Equal(userAddressEntity.AddressTitle, result.Data!.AddressTitle);

        await DisposeAsync();
    }


    [Fact]
    public async Task UpdateUserAddressEntityAsync_Should_Update_UserAddressEntity_In_Database()
    {
        // Arrange
        var userAddressEntity = new UserAddressEntity
        {
            UserId = "testUserId",
            AddressId = 1,
            AddressTitle = "Home"
        };

        await _userAddressRepository.AddAsync(userAddressEntity);

        // Modify the entity
        userAddressEntity.AddressTitle = "Work";

        var request = new ServiceRequest<UserAddressEntity> { Data = userAddressEntity };

        // Act
        var updatedEntity = await _addressService.UpdateUserAddressEntityAsync(request);

        // Assert
        Assert.NotNull(updatedEntity);
        Assert.Equal("Work", updatedEntity.Data!.AddressTitle);

        // Ensure the entity is updated in the database
        var updatedEntityInDatabase = await _userAddressRepository.GetAsync(x => x.UserId == userAddressEntity.UserId && x.AddressId == userAddressEntity.AddressId);
        Assert.NotNull(updatedEntityInDatabase);
        Assert.Equal("Work", updatedEntityInDatabase.AddressTitle);

        await DisposeAsync();
    }


    [Fact]
    public async Task DeleteUserAddressEntityAsync_Should_DeleteUserAddressEntity_From_Database()
    {
        // Arrange
        var userAddressEntity = new UserAddressEntity
        {
            UserId = "testUserId",
            AddressId = 1,
            AddressTitle = "Home"
        };

        await _userAddressRepository.AddAsync(userAddressEntity);

        var request = new ServiceRequest<UserAddressEntity> { Data = userAddressEntity };

        // Act
        var result = await _addressService.DeleteUserAddressEntityAsync(request);

        // Assert
        Assert.True(result.Data!);

        // Ensure the entity is deleted
        var deletedEntity = await _userAddressRepository.GetAsync(x => x.UserId == userAddressEntity.UserId && x.AddressId == userAddressEntity.AddressId);
        Assert.Null(deletedEntity);

        await DisposeAsync();
    }


    private async Task DisposeAsync()
    {
        await _identityContext.Database.EnsureDeletedAsync();
        _identityContext.Dispose();
    }
}
