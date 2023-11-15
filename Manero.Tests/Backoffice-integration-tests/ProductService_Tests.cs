﻿using Manero.Contexts;
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

    private readonly ProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly ProductRepository _productRepo;
    private readonly ProductCategoryRepository _productCategoryRepo;
    private readonly ImageService _imageService;
    private readonly CategoryRepository _categoryRepository;
    private readonly TagService _tagService;
    private readonly TagRepository _tagRepo;
    private readonly ProductTagRepository _productTagRepo;

    public ProductService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        _context = new DataContext(options);

        _categoryRepository = new CategoryRepository(_context);
        _categoryService = new CategoryService(_categoryRepository);

        _productCategoryRepo = new ProductCategoryRepository(_context);

        _tagRepo = new TagRepository(_context);
        _tagService = new TagService(_tagRepo);

        _productTagRepo = new ProductTagRepository(_context);

        //_imageService = new ImageService();


        _repository = new ProductRepository(_context);
        _productRepo = new ProductRepository(_context);
        _service = new ProductService(_repository, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);
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

    [Fact]
    public async Task GetAllProductsAsync_Should_ReturnAllProducts()
    {
        // Arrange
        var product1 = new ProductModel() { ArticleNumber = "Test-1", ProductName = "Test1", ProductPrice = 10 };
        var product2 = new ProductModel() { ArticleNumber = "Test-2", ProductName = "Test2", ProductPrice = 20 };

        await _context.Products.AddRangeAsync(product1, product2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); 
        Assert.Contains(result, p => p.ArticleNumber == "Test-1"); 
        Assert.Contains(result, p => p.ArticleNumber == "Test-2"); 
    }
}
