using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Manero.Tests.Backoffice_integration_tests;

public class ProductColorService_Tests
{

    private readonly ProductColorService _service;
    private readonly ProductColorRepository _repository;
    private readonly DataContext _context;

    public ProductColorService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _repository = new ProductColorRepository(_context);
        _service = new ProductColorService(_repository);
    }

    [Fact]
    public async Task AssociateColorsWithProductAsync_WithValidData_ShouldAddProductColors()
    {
        // Arrange
        var selectedColors = new List<ColorModel> {
            new ColorModel { Id = 1, Color = "Red" },
            new ColorModel { Id = 2, Color = "Blue" } 
        };
        var product = new ProductModel { ArticleNumber = "12345" };

        // Act
        await _service.AssociateColorsWithProductAsync(selectedColors, product);

        // Assert
        var productColors = await _service.GetProductWithColorsAsync();
        Assert.Equal(2, productColors.Count());
        Assert.All(productColors, pc =>
        {
            Assert.Equal(product.ArticleNumber, pc.ArticleNumber);
            Assert.Contains(pc.ColorId, selectedColors.Select(c => c.Id));
        });
    }
}
