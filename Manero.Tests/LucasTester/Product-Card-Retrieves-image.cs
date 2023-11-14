using Manero.Controllers;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Manero.Tests.LucasTester
{
    public class ShopControllerTests
    {
        [Fact]
        public async Task Index_Should_Add_MainImage_To_Product()
        {
            // Arrange
            // Mock services
            var mockImageService = new Mock<ImageService>();
            var mockProductService = new Mock<ProductService>();
            var mockTagService = new Mock<TagService>();
            var mockProductTagService = new Mock<ProductTagService>();
            var mockCategoryService = new Mock<CategoryService>();
            var mockProductCategoryService = new Mock<ProductCategoryService>();
            var mockSizeService = new Mock<SizeService>();
            var mockProductSizeService = new Mock<ProductSizeService>();
            var mockColorService = new Mock<ColorService>();
            var mockProductColorService = new Mock<ProductColorService>();

            // Test Product And viewModel
            var product = new ProductModel { ArticleNumber = "YourArticleNumber" };
            var viewModel = new ShopViewModel { Products = new List<ProductModel>() };

            // the expected main image
            var expectedImage = new ImageModel();

            // Setting up service method to return expected data
            mockImageService.Setup(x => x.GetMainImageAsync(product.ArticleNumber)).ReturnsAsync(expectedImage);

            var controller = new ShopController(
                mockProductService.Object,
                mockTagService.Object, // Add missing services
                mockProductTagService.Object,
                mockCategoryService.Object,
                mockProductCategoryService.Object,
                mockSizeService.Object,
                mockProductSizeService.Object,
                mockImageService.Object,
                mockColorService.Object,
                mockProductColorService.Object
            );

            // Act
            var result = await controller.Index(viewModel) as ViewResult;

            // Assert
            // Check if the result is not null
            Assert.NotNull(result);
            var model = result.Model as ShopViewModel;
            // Check if the model is not null
            Assert.NotNull(model);

            // Check if the product has been added to the ViewModel
            Assert.Single(model.Products);

            var productInModel = model.Products[0]; // Get the product
            Assert.NotNull(productInModel); // Check if the product is not null

            // Check if the main image has been added to the product
            Assert.Single(productInModel.Images); // Assuming only one image is added
            Assert.Equal(expectedImage, productInModel.Images[0]); // Check if the added image is the expected one
        }
    }
}
