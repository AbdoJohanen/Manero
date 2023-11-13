using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Manero.Tests.Backoffice_integration_tests;

public class ProductSizeService_Tests
{
    private readonly ProductSizeService _service;
    private readonly ProductSizeRepository _repository;
    private readonly DataContext _context;

    public ProductSizeService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new DataContext(options);
        _repository = new ProductSizeRepository(_context);
        _service = new ProductSizeService(_repository);

    }


    [Fact]
    public async Task AssociateSizesWithProductAsync_Should_AssociateOneProductModelWithListOfSizeModel()
    {
        //Arrange
        var sizes = new List<SizeModel>
        {
            new SizeModel { Id = 1, Size = "Small" },
            new SizeModel { Id = 2, Size = "Medium" },
            new SizeModel { Id = 3, Size = "Large" } 
        };
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test", ProductPrice = 0 };

        //Act
        await _service.AssociateSizesWithProductAsync(sizes, product);
        var productSizes = await _repository.GetAllAsync(x => x.ArticleNumber == product.ArticleNumber);

        //Assert
        Assert.NotNull(productSizes);
        foreach (var item in productSizes)
            Assert.Equal(item.ArticleNumber, product.ArticleNumber);

        Assert.Equal(sizes.Count(), productSizes.Count());
    }

    [Fact]
    public async Task AssociateSizesWithProductAsync_Should_AssociateMultipleSizesWithProduct()
    {
        // Arrange
        var sizes = new List<SizeModel>
    {
        new SizeModel { Id = 1, Size = "Small" },
        new SizeModel { Id = 2, Size = "Medium" },
        new SizeModel { Id = 3, Size = "Large" }
    };
        var product = new ProductModel
        {
            ArticleNumber = "Test-2",
            ProductName = "Test",
            ProductPrice = 0
        };

        // Act
        await _service.AssociateSizesWithProductAsync(sizes, product);
        var productSizes = await _repository.GetAllAsync(x => x.ArticleNumber == product.ArticleNumber);

        // Assert
        Assert.NotNull(productSizes);
        Assert.Equal(sizes.Count(), productSizes.Count());

        foreach (var size in sizes)
        {
            Assert.Contains(productSizes, ps => ps.SizeId == size.Id);
        }
    }
}
