using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Moq;

namespace Manero.Tests.LucasTester;

public class GetAllProducts_ReturnsProductTest
{
    private readonly ProductService _productService;
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
    private readonly ProductTagService _productTagService;
    private readonly ProductCategoryService _productCategoryService;
    private readonly ProductSizeService _productSizeService;
    private readonly ProductSizeRepository _productSizeRepository;
    private readonly ProductColorService _productColorService;
    private readonly ProductColorRepository _productColorRepository;
    private readonly ShopController _shopController;

    public GetAllProducts_ReturnsProductTest()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        _context = new DataContext(options);
        _productRepo = new ProductRepository(_context);
        _categoryRepository = new CategoryRepository(_context);
        _categoryService = new CategoryService(_categoryRepository);
        _productCategoryRepo = new ProductCategoryRepository(_context);
        var imageRepositoryContext = new DataContext(options);
        _sizeRepository = new SizeRepository(_context);
        _colorRepository = new ColorRepository(_context);
        _colorService = new ColorService(_colorRepository);
        _reviewRepository = new ReviewRepository(_context);
        _reviewService = new ReviewService(_reviewRepository);

        // Skapar en faktisk instans av TestWebHostEnvironment
        _webHostEnvironment = new TestWebHostEnvironment();

        // Skapar en faktisk instans av DataContext för ImageRepository
        _imageService = new ImageService(_webHostEnvironment, new ImageRepository(imageRepositoryContext));
        _tagRepo = new TagRepository(_context);
        _tagService = new TagService(_tagRepo);
        _productTagRepo = new ProductTagRepository(_context);
        _productTagService = new ProductTagService(_productTagRepo);
        _productCategoryService = new ProductCategoryService(_productCategoryRepo);
        _sizeService = new SizeService(_sizeRepository);
        _productSizeRepository = new ProductSizeRepository(_context);
        _productSizeService = new ProductSizeService(_productSizeRepository);
        _productColorRepository = new ProductColorRepository(_context);
        _productColorService = new ProductColorService(_productColorRepository);
        _productService = new ProductService(_productRepo, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);

        _shopController = new ShopController(_productService, _tagService, _productTagService, _categoryService, _productCategoryService, _sizeService, _productSizeService, _imageService, _colorService, _productColorService);
    }

    [Fact]
    public async Task Index_ReturnsViewWithProducts()
    {
        // Arrange
        var product = new ProductModel { ArticleNumber = "123", ProductName = "Product 1", ProductPrice = 250 };
        await _context.Products.AddRangeAsync(product);
        await _context.SaveChangesAsync();

        var allProducts = await _productService.GetAllProductsAsync();


        ShopViewModel viewModel = new();
        foreach (var productModel in allProducts)
        {
            viewModel.Products.Add(productModel);
        }

        //ShopViewModel viewModel = new()
        //{
        //    Products = new List<ProductModel> { product }
        //};

        var controller = new ShopController(_productService, _tagService, _productTagService, _categoryService, _productCategoryService, _sizeService, _productSizeService, _imageService, _colorService, _productColorService);

        // Act

        var result = await controller.Index(viewModel);


        // Assert
        Assert.NotNull(result);
    }


    //[Fact]
    //public async Task ProductsController_Should_Return_Correct_View()
    //{
    //    //Arrange
    //    //Adding a test product to our in-memory database
    //    var product = new ProductEntity { ArticleNumber = "1", ProductName = "Product 1", ProductPrice = 10 };

    //    await _context.Products.AddRangeAsync(product);
    //    await _context.SaveChangesAsync();

    //    //Act
    //    //Retrieving the test product from our in-memory database to convert it to a productmodel, which is then used in the controller
    //    var productModel = await _service.GetProductWithImagesAsync(product.ArticleNumber);
    //    var result = await _productsController.ProductDetails(productModel.ArticleNumber) as ViewResult;

    //    //Assert
    //    Assert.NotNull(result); // Ensure that a ViewResult is returned
    //    Assert.IsType<ViewResult>(result); // Check the type of the result

    //    // Ensure that the properties of productModel matches the expected values
    //    Assert.Equal("1", productModel.ArticleNumber);
    //    Assert.Equal("Product 1", productModel.ProductName);
    //    Assert.Equal(10, productModel.ProductPrice);
    //}


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
            //OBS: Här behöver ni ändra till sökvägen för wwwroot på eran dator för att det ska fungera. 
            WebRootPath = Path.Combine("C:\\Users\\abdoj\\OneDrive\\Skrivbord\\EC_Utbildning\\Projekt\\Manero\\Manero\\wwwroot");

            WebRootFileProvider = new PhysicalFileProvider(WebRootPath);
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
        }
    }

    //[Fact]
    //public async Task Index_ReturnsViewWithProducts()
    //{
    //    // Arrange
    //    // Mock services
    //    //var mockImageService = new Mock<ImageService>();
    //    //var mockProductService = new Mock<ProductService>();
    //    //var mockTagService = new Mock<TagService>();
    //    //var mockProductTagService = new Mock<ProductTagService>();
    //    //var mockCategoryService = new Mock<CategoryService>();
    //    //var mockProductCategoryService = new Mock<ProductCategoryService>();
    //    //var mockSizeService = new Mock<SizeService>();
    //    //var mockProductSizeService = new Mock<ProductSizeService>();
    //    //var mockColorService = new Mock<ColorService>();
    //    //var mockProductColorService = new Mock<ProductColorService>();

    //    //var controller = new ShopController(
    //    //    mockProductService.Object,
    //    //    mockTagService.Object,
    //    //    mockProductTagService.Object,
    //    //    mockCategoryService.Object,
    //    //    mockProductCategoryService.Object,
    //    //    mockSizeService.Object,
    //    //    mockProductSizeService.Object,
    //    //    mockImageService.Object,
    //    //    mockColorService.Object,
    //    //    mockProductColorService.Object
    //    //);

    //    // Test viewModel
    //    var viewModel = new ShopViewModel();

    //    // Mock behavior for GetAllProductsAsync
    //    var mockProducts = new List<ProductModel>
    //{
    //    new ProductModel { ArticleNumber = "123", ProductPrice = 250}
    //};

    //    mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(mockProducts);

    //    // Act
    //    var result = await controller.Index(viewModel);

    //    // Assert
    //    Assert.IsType<ViewResult>(result);
    //    var viewResult = Assert.IsType<ViewResult>(result);
    //    Assert.IsType<ShopViewModel>(viewResult.Model);

    //    var model = (ShopViewModel)viewResult.Model;
    //    Assert.NotNull(model.Products);
    //    Assert.Equal(mockProducts.Count, model.Products.Count);
    //}
}
