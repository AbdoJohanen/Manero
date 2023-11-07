using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manero.Tests.GetProductNameTests;

namespace Manero.Tests.Malin_Mira
{
    public class GetProductImagesTest
    {

        private readonly ImageService _imageService;
        private readonly ImageRepository _imageRepository;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GetProductImagesTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _webHostEnvironment = new TestWebHostEnvironment();
            var imageRepositoryContext = new DataContext(options);
            _imageService = new ImageService(_webHostEnvironment, new ImageRepository(imageRepositoryContext));
            _imageRepository = new ImageRepository(imageRepositoryContext);
        }

        [Fact]
        public async Task GetAllImagesAsync_Should_Return_Images()
        {

            // Arrange
            var image1 = new ImageEntity { Id = "bde81c1d-0cc3-40ba-9b81-b08c8b8f1107", ImageUrl = "bde81c1d-0cc3-40ba-9b81-b08c8b8f1107-image1.jpg", ProductArticleNumber = "101", IsMainImage = true };
            var image2 = new ImageEntity { Id = "36295641-f7a6-4207-9695-25b8d5c40e81", ImageUrl = "36295641-f7a6-4207-9695-25b8d5c40e81-image2.jpg", ProductArticleNumber = "101", IsMainImage = false };
            var image3 = new ImageEntity { Id = "3e540a53-5677-40f8-b39e-9507576a8b71", ImageUrl = "3e540a53-5677-40f8-b39e-9507576a8b71-image3.jpg", ProductArticleNumber = "101", IsMainImage = false };

            await _context.Images.AddRangeAsync(image1, image2, image3);
            await _context.SaveChangesAsync();

            //Act
            var result = await _imageService.GetAllImagesAsync(image1.ProductArticleNumber);

            //Assert
            // Assert: Kontrollera att resultatet är en lista med kategorimodeller i rätt format
            Assert.NotNull(result);
            Assert.IsType<List<ImageModel>>(result);

            // Kontrollera att antalet kategorimodeller matchar antalet kategorier i databasen
            Assert.Equal(3, result.Count());

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
}
