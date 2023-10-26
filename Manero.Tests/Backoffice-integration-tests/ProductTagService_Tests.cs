using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.Backoffice_integration_tests;

public class ProductTagService_Tests
{
    private readonly ProductTagService _service;
    private readonly ProductTagRepository _repository;
    private readonly DataContext _context;

    public ProductTagService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new DataContext(options);
        _repository = new ProductTagRepository(_context);
        _service = new ProductTagService(_repository);
    }

    [Fact]
    public async Task AssociateTagsWithProductAsync_Should_AssociateOneProductModelWithListOfTagModel()
    {
        //Arrange
        var tags = new List<TagModel>() { new TagModel { Id = 1, Tag = "Test" }, new TagModel { Id = 2, Tag = "Test" } };
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test", ProductPrice = 0 };

        //Act
        await _service.AssociateTagsWithProductAsync(tags, product);
        var productTags = await _repository.GetAllAsync(x => x.ArticleNumber == product.ArticleNumber);

        //Assert
        Assert.NotNull(productTags);
        foreach (var item in productTags)
            Assert.Equal(item.ArticleNumber, product.ArticleNumber);

        Assert.Equal(tags.Count(), productTags.Count());
    }
}
