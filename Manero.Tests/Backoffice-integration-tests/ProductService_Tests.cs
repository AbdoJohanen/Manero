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

    [Fact]
    public async Task GetProductAsync_Should_ReturnProductModel_When_ProductExistsInDatabase()
    {
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test" };
        await _service.CreateProductAsync(product);

        // Act
        var result = await _service.GetProductAsync(product.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductModel>(result);
        Assert.Equal(product.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task DeleteProductAsync_Should_ReturnTrue_If_ProductIsDeleted()
    {
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test", ProductPrice = 0 };
        await _service.CreateProductAsync(product);

        // Act
        var result = await _service.DeleteProductAsync(product.ArticleNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateProductAsync_should_ReturnUpdatedProduct_When_UpdatedSuccessfully()
    {   
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test" };
        var newProduct = new ProductModel() { ProductName = "Test-2" };
        await _service.CreateProductAsync(product);

        // Act
        var result = await _service.UpdateProductAsync(newProduct, product.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductModel>(result);
        Assert.NotEqual(product.ProductName, result.ProductName);
        Assert.Equal(product.ArticleNumber, result.ArticleNumber);
    }
}
