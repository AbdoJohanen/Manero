using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Manero.Tests.Backoffice_integration_tests;

public class ColorService_Tests
{
    private readonly ColorRepository _repository;
    private readonly ColorService _service;
    private readonly DataContext _context;

    public ColorService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _repository = new ColorRepository(_context);
        _service = new ColorService(_repository);
    }


    [Fact]
    public async Task GetAllColorsAsync_ShouldReturnColors()
    {
        // Arrange
        _context.Colors.Add(new ColorEntity { Id = 1, Color = "Red", ColorsProducts = null! });
        _context.Colors.Add(new ColorEntity { Id = 2, Color = "Blue", ColorsProducts = null! });
        _context.SaveChanges();

        // Act
        var result = await _service.GetAllColorsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.NotEqual(3, result.Count());
    }
}
