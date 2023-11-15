using Manero.Controllers;
using Manero.Enums;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Identity;
using Manero.Models.Test;
using Manero.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Twilio.Rest.Numbers.V2.RegulatoryCompliance;

namespace Manero.Tests.UnitTests;

public interface IUserManager
{
    Task<IdentityResult> CreateAsync(AppUser user, string password);
}

public class TestUserManager : IUserManager
{
    public Task<IdentityResult> CreateAsync(AppUser user, string password)
    {
        return Task.FromResult(IdentityResult.Success);
    }
}

public class UserController_Test
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly FakeUserManager _fakeUserManager;
    private readonly FakeSignInManager _fakeSignInManager;
    private readonly AccountController _accountController;
    private readonly RegisterController _registerController;

    public UserController_Test()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockUserService = new Mock<IUserService>();
        _fakeUserManager = new FakeUserManager();
        _fakeSignInManager = new FakeSignInManager();
        _accountController = new AccountController(_mockAuthService.Object , _fakeUserManager, _mockUserService.Object);
        _registerController = new RegisterController(_mockAuthService.Object, _mockUserService.Object, _fakeSignInManager);
    }

    [Fact]
    public async Task AddUserAsync_Should_Return_BadRequest_When_ModelState_IsNotValid()
    {
        // Arrange
        var schema = new UserRegisterViewModel();
        _registerController.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _registerController.Index(schema);

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task AddUserAsync_Should_Return_Conflict_When_UserExists()
    {
        // Arrange
        var schema = new UserRegisterViewModel();
        var request = new ServiceRequest<AppUser> { Data = schema };
        var response = new ServiceResponse<bool>
        {
            Status = StatusCode.Conflict,
            Data = true
        };
        _mockAuthService.Setup(x => x.ExistUserAsync(It.IsAny<Expression<Func<AppUser, bool>>>())).ReturnsAsync(response);

        // Act
        var result = await _registerController.Index(schema);

        // Assert
        Assert.IsType<ConflictResult>(result);

        var content = result as ConflictResult;
        Assert.Equal(409, (int)content!.StatusCode );
    }
}
