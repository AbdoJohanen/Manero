using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

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
    private readonly ProductTagRepository _productTagRepo;
    private readonly ProductsController _productsController;
    private readonly IWebHostEnvironment _webHostEnvironment;

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
        _productTagRepo = new ProductTagRepository(_context);
        _productsController = new ProductsController(_service);

        _service = new ProductService(_productRepo, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);
    }


    public async Task ProductsController_Should_Return_Correct_View()
    {


        //Arrange
        var product = new ProductEntity { ArticleNumber = "1", ProductName = "Product 1", ProductPrice = 10 };

        await _context.Products.AddRangeAsync(product);
        await _context.SaveChangesAsync();


        //Act
        var productModel = await _service.GetProductWithImagesAsync(product.ArticleNumber);
        var result = await _productsController.Index(productModel.ArticleNumber) as ViewResult;

        // Assert
        Assert.NotNull(result); // Ensure that a ViewResult is returned
        Assert.IsType<ViewResult>(result); // Check the type of the result

        // Assert that the view name is the expected view name (e.g., "Index")
        Assert.Equal("Index", result.ViewName);

        // Assert that the model passed to the view is the productModel
        Assert.Equal(productModel, result.Model);
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

            WebRootPath = Path.Combine("C:\\Users\\sangs\\Documents\\Webbutvecklare\\Projekt\\Manero\\Manero\\Manero\\wwwroot");

            WebRootFileProvider = new PhysicalFileProvider(WebRootPath);
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
        }
    }
}
