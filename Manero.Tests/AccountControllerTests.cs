using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Tests;

public class AccountControllerTests
{
    private readonly AccountController _accountController;

    [Fact]
    public async Task AddAddress_Should_AddAddressEntityAndUserAddressentity_To_AzureSqlDatabase()
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

        // Assert

    }
}
