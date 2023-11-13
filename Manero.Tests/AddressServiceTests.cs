using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Tests;

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
        var result = await _addressService.GetorCreateAsync(addressEntity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<AddressEntity>(result);
        Assert.Equal(addressEntity.Id, result.Id);
        Assert.Equal(addressEntity.StreetName, result.StreetName);
        Assert.Equal(addressEntity.PostalCode, result.PostalCode);
        Assert.Equal(addressEntity.City, result.City);

        await DisposeAsync();
    }


    [Fact]
    public async Task GetuserAddressEntityByIdAsync_Should_ReturnUserAddressEntity_When_AddressIdExists()
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
        Assert.IsType<UserAddressEntity>(result);
        Assert.Equal(userAddressEntity.UserId, result.UserId);
        Assert.Equal(userAddressEntity.AddressId, result.AddressId);
        Assert.Equal(userAddressEntity.AddressTitle, result.AddressTitle);

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

        // Act
        var updatedEntity = await _addressService.UpdateUserAddressEntityAsync(userAddressEntity);

        // Assert
        Assert.NotNull(updatedEntity);
        Assert.Equal("Work", updatedEntity.AddressTitle);

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

        // Act
        var result = await _addressService.DeleteUserAddressEntityAsync(userAddressEntity);

        // Assert
        Assert.True(result);

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
