using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace Manero.Tests.AbdoTests;

public class AccountControllerTests
{
    private readonly AddressService _addressService;
    private readonly IdentityContext _identityContext;
    private readonly AddressRepository _addressRepository;
    private readonly UserAddressRepository _userAddressRepository;
    private readonly AuthService _authService;
    private readonly UserManager<AppUser> _userManager;
    private readonly UserService _userService;


    public AccountControllerTests()
    {
        var options = new DbContextOptionsBuilder<IdentityContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

        _identityContext = new IdentityContext(options);
        _addressRepository = new AddressRepository(_identityContext);
        _userAddressRepository = new UserAddressRepository(_identityContext);
        _addressService = new AddressService(_addressRepository, _userAddressRepository);
        _userManager = new UserManager<AppUser>(new Mock<IUserStore<AppUser>>().Object, null!, null!, null!, null!, null!, null!, null!, null!);

    }

    [Fact]
    public async Task AccountAddressViewModel_Should_ReturnExpectedData()
    {
        // Arrange
        await _identityContext.AspNetAddresses.AddAsync(new AddressEntity { Id = 1, StreetName = "Addressvägen 1", PostalCode = "12345", City = "Örebro" });
        await _identityContext.AspNetUsersAddresses.AddAsync(new UserAddressEntity { UserId = "userId", AddressId = 1, AddressTitle = "HomeTest" });
        await _identityContext.SaveChangesAsync();

        // Creates a ClaimsPrincipal for a user with a specific claim (for testing purposes).
        ClaimsPrincipal user = new(new ClaimsIdentity(new[]
        {
            // Creating a claim with type ClaimTypes.NameIdentifier and value "userId".
            new Claim(ClaimTypes.NameIdentifier, "userId")
        }));


        // Creates an instance of the AccountController for testing
        var controller = new AccountController(_authService, _userManager, _userService, _addressService)
        {
            ControllerContext = new ControllerContext
            {
                // Creates a DefaultHttpContext with a simulated user that we created before
                HttpContext = new DefaultHttpContext { User = user }
            }
        };

        // Act
        var result = await controller.Address();


        // Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = result as ViewResult;
        var viewModel = viewResult!.Model as AddressViewModel;
        Assert.NotNull(viewModel);
    }
}
