using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Manero.Tests.Malin_Mira;

public class GetProductNameTests

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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetProductNameTests()
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

        _service = new ProductService(_productRepo, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);
    }

    [Fact]
    public async Task GetProductWithImagesAsync_ShouldReturnProductName()
    {
        //Arrange
        //Adding a test product to our in-memory database
        var product = new ProductEntity { ArticleNumber = "1", ProductName = "Product 1", ProductPrice = 10 };

        await _context.Products.AddRangeAsync(product);
        await _context.SaveChangesAsync();

        //Act
        //Retrieving the test product from our in-memory database
        var result = await _service.GetProductWithImagesAsync(product.ArticleNumber);

        //Assert
        //Ensure that the retrieved ProductName property is equal to the product name added in in-memory database
        Assert.Equal("Product 1", result.ProductName);
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





