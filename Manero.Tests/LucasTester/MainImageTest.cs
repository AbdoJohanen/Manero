using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Manero.Tests.LucasTester
{
    public class MainImageTest
    {
        private readonly ImageService _imageService;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MainImageTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new DataContext(options);
            _webHostEnvironment = new TestWebHostEnvironment();
            var imageRepositoryContext = new DataContext(options);
            _imageService = new ImageService(_webHostEnvironment, new ImageRepository(imageRepositoryContext));

        }


        [Fact]
        public async Task MainImageShouldReturnTrueOrFalse()
        {

            //Arrange - Sätter upp 2 produkter, en med main image och en utan
            var productWithMainImage = new ProductModel
            {
                Images = new List<ImageModel>
            {
                new ImageModel
                {
                    Id = "123",
                    ImageUrl = "testUrl",
                    IsMainImage = true
                }
            }
            };

            var productWithoutMainImage = new ProductModel
            {
                Images = new List<ImageModel>
            {
                new ImageModel
                {
                    Id = "928",
                    ImageUrl = "anotherUrl",
                    IsMainImage = false
                }
            }
            };

            //Act - Skapar 2 variabler för vardera test produkt
            var hasMainImage = productWithMainImage.Images;
            var hasNoMainImage = productWithoutMainImage.Images;

            //Assert - Verifierar om "hasMainImage" returnerar true och om "hasNoMainImage" returnerar false
            Assert.True(hasMainImage[0].IsMainImage);
            Assert.False(hasNoMainImage[0].IsMainImage);
        }
    }
}
