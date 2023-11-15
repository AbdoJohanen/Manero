using Manero.Contexts;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Tests.Elvins_Tester.IntegrationTests;

public class UserRepository_Test
{
    private readonly IdentityContext _context;
    private readonly IUserRepository _repository;

    public UserRepository_Test()
    {
        var options = new DbContextOptionsBuilder<IdentityContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new IdentityContext(options);
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task AddUserAsync_Should_AddEntity_toDatabase_and_ReturnEntity()
    {
        //Arrange
        var entity = new AppUser { Name = "Elvins Test" };

        //Act
        var result = await _repository.AddAsync(entity);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<AppUser>(result);
        Assert.Equal(entity.Name, result.Name);
    }

    [Fact]
    public async Task UpdateUserAsync_Should_ReturnTrue_when_UserDetailsUpdates()
    {
        //Arrange
        var user = new AppUser { Name = "Test1" };
        await _repository.AddAsync(user);

        //Act
        user.Name = "Test-2";
        var result = await _repository.UpdateAsync(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<AppUser>(result);
        Assert.Equal("Test-2", result.Name);
    }
}
