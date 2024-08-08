using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Manero.Tests.LucasTester
{


    // Test för shop controller - Kollar om shopViewModel returnerar korrekt data
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
            // Denna path måste anges utefter användarens dator, annars fungerar inte testet
            WebRootPath = Path.Combine("C:\\Users\\georg\\OneDrive\\Skrivbord\\ManeroAgile\\Manero\\Manero\\wwwroot");

            WebRootFileProvider = new PhysicalFileProvider(WebRootPath);
            ContentRootFileProvider = new PhysicalFileProvider(ContentRootPath);
        }
    }
    public class ShopControllerTest
    {
        [Fact]
        public async Task ShopIndexViewModel_ReturnsExpectedData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new DataContext(options);

            // Lägg till testprodukter i in-memory-databasen
            context.Products.Add(new ProductModel { ArticleNumber = "82828", ProductName = "TestProduct 1" });
            context.Products.Add(new ProductModel { ArticleNumber = "138549", ProductName = "TestProduct 2" });

            context.SaveChanges();

            var productService = new ProductService(
                new ProductRepository(context),
                new CategoryService(new CategoryRepository(context)),
                new ProductRepository(context),
                new ProductCategoryRepository(context),
                new ImageService(new TestWebHostEnvironment(), new ImageRepository(context)),
                new CategoryRepository(context),
                new TagService(new TagRepository(context)),
                new TagRepository(context),
                new ProductTagRepository(context)
            );

            var imageService = new ImageService(new TestWebHostEnvironment(), new ImageRepository(context));

            var controller = new ShopController(productService, imageService);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            var viewModel = viewResult!.Model as ShopViewModel;

            Assert.NotNull(viewModel.Items);
        }

    }
}




