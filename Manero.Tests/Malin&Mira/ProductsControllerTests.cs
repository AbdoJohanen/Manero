﻿using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Manero.Tests.Malin_Mira;

public class ProductsControllerTests
{
    private readonly ProductService _service;
    private readonly ProductRepository _productRepo;
    private readonly DataContext _context;
    private readonly CategoryService _categoryService;
    private readonly ProductCategoryRepository _productCategoryRepo;
    private readonly ImageService _imageService;
    private readonly CategoryRepository _categoryRepository;
    private readonly TagService _tagService;
    private readonly TagRepository _tagRepo;
    private readonly SizeService _sizeService;
    private readonly SizeRepository _sizeRepository;
    private readonly ColorRepository _colorRepository;
    private readonly ColorService _colorService;
    private readonly ProductTagRepository _productTagRepo;
    private readonly ProductsController _productsController;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ReviewService _reviewService;
    private readonly ReviewRepository _reviewRepository;

    public ProductsControllerTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

        _context = new DataContext(options);
        _productRepo = new ProductRepository(_context);
        _categoryRepository = new CategoryRepository(_context);
        _categoryService = new CategoryService(_categoryRepository);
        _productCategoryRepo = new ProductCategoryRepository(_context);
        _webHostEnvironment = new TestWebHostEnvironment();
        var imageRepositoryContext = new DataContext(options);
        _imageService = new ImageService(_webHostEnvironment, new ImageRepository(imageRepositoryContext));
        _tagRepo = new TagRepository(_context);
        _tagService = new TagService(_tagRepo);
        _sizeRepository = new SizeRepository(_context);
        _sizeService = new SizeService(_sizeRepository);
        _colorRepository = new ColorRepository(_context);
        _colorService = new ColorService(_colorRepository);
        _productTagRepo = new ProductTagRepository(_context);
        _reviewRepository = new ReviewRepository(_context);
        _reviewService = new ReviewService(_reviewRepository);
        _service = new ProductService(_productRepo, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);
        _productsController = new ProductsController(_service, _sizeService, _tagService, _categoryService, _colorService, _reviewService);
    }

    [Fact]
    public async Task ProductsController_Should_Return_Correct_View()
    {
        //Arrange
        //Adding a test product to our in-memory database
        var product = new ProductEntity { ArticleNumber = "1", ProductName = "Product 1", ProductPrice = 10 };

        await _context.Products.AddRangeAsync(product);
        await _context.SaveChangesAsync();

        //Act
        //Retrieving the test product from our in-memory database to convert it to a productmodel, which is then used in the controller
        var productModel = await _service.GetProductWithImagesAsync(product.ArticleNumber);
        var result = await _productsController.ProductDetails(productModel.ArticleNumber) as ViewResult;

        //Assert
        Assert.NotNull(result); // Ensure that a ViewResult is returned
        Assert.IsType<ViewResult>(result); // Check the type of the result

        // Ensure that the properties of productModel matches the expected values
        Assert.Equal("1", productModel.ArticleNumber);
        Assert.Equal("Product 1", productModel.ProductName);
        Assert.Equal(10, productModel.ProductPrice);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public class TestWebHostEnvironment : IWebHostEnvironment
    {
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; }
        public string EnvironmentName { get; set; } = "Development";
        public string ApplicationName { get; set; } = "Manero";

        public TestWebHostEnvironment()
        {
            WebRootPath = Path.Combine("C:\\Users\\georg\\OneDrive\\Skrivbord\\ManeroAgile\\Manero\\Manero\\wwwroot");

            WebRootFileProvider = new PhysicalFileProvider(WebRootPath);
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
        }
    }
}
