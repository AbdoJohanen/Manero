using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Tests;

public class AccountControllerTests
{
    private readonly AddressService _addressService;
    private readonly AccountController _accountController;
    private readonly IdentityContext _identityContext;

    public AccountControllerTests(AddressService addressService, AccountController accountController, IdentityContext identityContext)
    {
        var options = new DbContextOptionsBuilder<IdentityContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

        _addressService = addressService;
        _accountController = accountController;
        _identityContext = identityContext;
    }

    [Fact]
    public async Task AddAddress_Should_AddAddressEntityAndUserAddressEntity_To_SqlDatabase()
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
        Assert.Equal(addressEntity.StreetName, result.StreetName);




    }
}
