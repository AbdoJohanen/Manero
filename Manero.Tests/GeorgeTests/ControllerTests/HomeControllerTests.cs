using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Manero.Tests.GeorgeTests.GetAllIntegrationTests.ProductServiceIntegrationTests;

namespace Manero.Tests.GeorgeTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task HomeIndexViewModel_ReturnsExpectedData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new DataContext(options);

            // Lägg till testprodukter i in-memory-databasen
            context.Products.Add(new ProductModel { ArticleNumber = "1", ProductName = "Product 1" });
            context.Products.Add(new ProductModel { ArticleNumber = "2", ProductName = "Product 2" });

            // Lägg till Tags på produkter
            context.Tags.Add(new TagModel { Tag = "Best Sellers" });
            context.Tags.Add(new TagModel { Tag = "Featured Products" });

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
            var categoryService = new CategoryService(new CategoryRepository(context));

            var controller = new HomeController(productService, categoryService);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            var viewModel = viewResult!.Model as HomeIndexViewModel;

            Assert.NotNull(viewModel);
            Assert.NotNull(viewModel.Featured);
            Assert.NotNull(viewModel.BestSelling);

            Assert.All(viewModel.Featured.GridItems, product =>
            {
                Assert.True(product.Tags.Any(tag => tag.Tag.Contains("Featured Products")), $"Product {product.ProductName} should have the 'Featured Products' tag.");
            });

        }

    }
}

