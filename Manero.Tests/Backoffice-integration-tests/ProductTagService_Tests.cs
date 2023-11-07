using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;

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

    [Fact]
    public async Task GetProductWithTagsAsync_Should_ReturnListOfProductTagModel_When_ExecutedCorrectly()
    {
        // Arrange
        var productTag = new ProductTagModel() { ArticleNumber = "Test-1", TagId = 1 };
        await _repository.AddAsync(productTag);

        // Act
        var result = await _service.GetProductWithTagsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ProductTagModel>>(result);
    }

    [Fact]
    public async Task UpdateProductTagsAsync_Should_DeleteCurrentTagsForProduct_Then_AddNewOnesWithProvidedInformation()
    {
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test" };
        var currentTags = new List<TagModel>() { new TagModel { Id = 1, Tag = "Tag-1" }, new TagModel { Id = 4, Tag = "Tag-4" } };
        await _service.AssociateTagsWithProductAsync(currentTags, product);
        var newTags = new List<int>() { 2, 3 };

        // Act
        var result = await _service.UpdateProductTagsAsync(product.ArticleNumber, newTags);
        var resultTagIds = result.Select(pt => pt.TagId).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newTags, resultTagIds);

        foreach (var productTag in result)
            foreach (var tag in currentTags)
                Assert.NotEqual(tag.Id, productTag.TagId);
    }
}
