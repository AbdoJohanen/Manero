using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.Backoffice_integration_tests;

public class ProductService_Tests
{
    private readonly ProductService _service;
    private readonly ProductRepository _repository;
    private readonly DataContext _context;

    public ProductService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new DataContext(options);
        _repository = new ProductRepository(_context);
        _service = new ProductService(_repository);
    }

    [Fact]
    public async Task CreateProductAsync_Should_ReturnProductModel_When_CreatedSuccessfully()
    {
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test", ProductPrice = 0 };

        // Act
        var result = await _service.CreateProductAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductModel>(result);
    }
}
