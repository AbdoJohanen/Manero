using Manero.Contexts;
using Manero.Controllers;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manero.Tests.GetAllIntegrationTests.ProductServiceIntegrationTests;

namespace Manero.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task HomeIndexViewModel_ReturnsExpectedData()
        {
            // Arrange
            // Skapa en in-memory-databas för teständamål
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new DataContext(options); // Använd en using-sats för att säkerställa att databasen rensas efter testet

            // Lägg till några testprodukter i in-memory-databasen
            context.Products.Add(new ProductModel { ArticleNumber = "1", ProductName = "Product 1" });
            context.Products.Add(new ProductModel { ArticleNumber = "2", ProductName = "Product 2" });
            context.SaveChanges();

            // Skapa en ProductService som använder in-memory-databasen
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

            var controller = new HomeController(productService);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result); // Se till att det är en ViewResult

            var viewResult = result as ViewResult;
            var viewModel = viewResult.Model as HomeIndexViewModel;


            Assert.NotNull(viewModel);
            Assert.NotNull(viewModel.Featured);
            Assert.NotNull(viewModel.BestSelling);

            // Om du har specifika förväntningar på vilka produkter som ska finnas i listorna, verifiera dem här.

            // Exempel: 
            Assert.Contains(viewModel.Featured.GridItems, p => p.ProductName == "Product 1");
            Assert.Contains(viewModel.Featured.GridItems, p => p.ProductName == "Product 2");

        }
    }
}

