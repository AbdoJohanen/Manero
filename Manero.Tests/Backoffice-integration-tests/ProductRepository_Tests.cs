using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.Backoffice_integration_tests;

public class ProductRepository_Tests
{
    private readonly DataContext _context;
    private readonly ProductRepository _repository;

    public ProductRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _repository = new ProductRepository(_context);
    }

    [Fact]
    public async Task AddAsync_Should_SaveEntityToDatabase_And_ReturnEntity()
    {
        // Arrange
        var entity = new ProductEntity() { ArticleNumber = "Test-1", ProductName = "Test", ProductPrice = 0 };

        // Act
        var result = await _repository.AddAsync(entity);
        var fromDataBase = await _repository.GetAsync(x => x.ArticleNumber == entity.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductEntity>(result);
        Assert.Equal(entity.ProductName, fromDataBase.ProductName);
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnListOfProductEntity()
    {
        // Arrange
        var entities = new List<ProductEntity>()
        {
            new ProductEntity
            {
                ArticleNumber = "1",
                ProductName = "Test",
                ProductPrice = 0
            },
            new ProductEntity
            {
                ArticleNumber= "2",
                ProductName = "Test",
                ProductPrice = 0
            }
        };

        foreach (var entity in entities)
            await _repository.AddAsync(entity);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
    }
}
