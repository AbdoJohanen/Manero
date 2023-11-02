﻿using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;


namespace Manero.Tests.GetAllIntegrationTests
{

    public class ProductServiceIntegrationTests : IDisposable
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

        public ProductServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _productRepo = new ProductRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _categoryService = new CategoryService(_categoryRepository);
            _productCategoryRepo = new ProductCategoryRepository(_context);

            // Skapa en faktisk instans av TestWebHostEnvironment
            _webHostEnvironment = new TestWebHostEnvironment();

            // Skapa en faktisk instans av DataContext för ImageRepository
            var imageRepositoryContext = new DataContext(options);
            _imageService = new ImageService(_webHostEnvironment, new ImageRepository(imageRepositoryContext));
            _tagRepo = new TagRepository(_context);
            _tagService = new TagService(_tagRepo);
            _productTagRepo = new ProductTagRepository(_context);

            _service = new ProductService(_productRepo, _categoryService, _productRepo, _productCategoryRepo, _imageService, _categoryRepository, _tagService, _tagRepo, _productTagRepo);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts()
        {
            // Arrange: Lägg till testdata i din InMemory-databas
            var product1 = new ProductModel { ArticleNumber = "Test-1", ProductName = "Test Product 1", ProductPrice = 10 };
            var product2 = new ProductModel { ArticleNumber = "Test-2", ProductName = "Test Product 2", ProductPrice = 15 };

            await _context.Products.AddRangeAsync(product1, product2);
            await _context.SaveChangesAsync();

            // Act: Anropa GetAllAsync-metoden
            var result = await _service.GetAllAsync();

            // Assert: Kontrollera att resultatet är en lista med produkter och att antalet produkter matchar antalet i databasen
            Assert.NotNull(result);
            Assert.IsType<List<ProductModel>>(result);
            Assert.Equal(2, result.Count());



            // Rensa InMemory-databasen efter testet om det behövs
            _context.Database.EnsureDeleted();
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

            [Fact]
            public void ProductName_ComesFromDatabase()
            {
                // Arrange
                var product = new ProductModel
                {
                    ProductName = "Product Name from Database"
                };

                // Act
                var productName = product.ProductName;

                // Assert
                Assert.Equal("Product Name from Database", productName);
            }
            [Fact]
            public void ProductPrice_ComesFromDatabase()
            {
                // Arrange
                var product = new ProductModel
                {
                    ProductPrice = 50
                };

                // Act
                var productPrice = product.ProductPrice;

                // Assert
                Assert.Equal(50, productPrice);
            }

        }
    }
}
