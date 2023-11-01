﻿using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.Backoffice_integration_tests;

public class ProductCategoryService_Tests
{
    private readonly ProductCategoryService _service;
    private readonly ProductCategoryRepository _repository;
    private readonly DataContext _context;

    public ProductCategoryService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new DataContext(options);
        _repository = new ProductCategoryRepository(_context);
        _service = new ProductCategoryService(_repository);
    }

    [Fact]
    public async Task UpdateProductCategoriesAsync_Should_UpdateCategoriesForProduct_Without_UpdatingArticleNumber()
    {
        // Arrange
        var productCategory = new ProductCategoryModel { ArticleNumber = "Test-1", CategoryId = 1 };
        await _repository.AddAsync(productCategory);
        var selectedCategories = new List<int> { 2, 3 };

        // Act
        var result = await _service.UpdateProductCategoriesAsync(productCategory.ArticleNumber, selectedCategories);

        // Assert
        Assert.NotNull(result);
        foreach (var item in result)
        {
            Assert.NotEqual(productCategory.CategoryId, item.CategoryId);
            Assert.Equal(productCategory.ArticleNumber, item.ArticleNumber);
        }
    }
}
