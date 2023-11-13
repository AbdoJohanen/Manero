using Manero.Controllers;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Manero.Tests
{
    public class GetAllProducts_ReturnsProductTest
    {
        [Fact]
        public async Task Index_ReturnsViewWithProducts()
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

            var controller = new ShopController(
                mockProductService.Object,
                mockTagService.Object,
                mockProductTagService.Object,
                mockCategoryService.Object,
                mockProductCategoryService.Object,
                mockSizeService.Object,
                mockProductSizeService.Object,
                mockImageService.Object,
                mockColorService.Object,
                mockProductColorService.Object
            );

            // Test viewModel
            var viewModel = new ShopViewModel();

            // Mock behavior for GetAllProductsAsync
            var mockProducts = new List<ProductModel>
        {
            new ProductModel { ArticleNumber = "123", ProductPrice = 250}
        };

            mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(mockProducts);

            // Act
            var result = await controller.Index(viewModel);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ShopViewModel>(viewResult.Model);

            var model = (ShopViewModel)viewResult.Model;
            Assert.NotNull(model.Products);
            Assert.Equal(mockProducts.Count, model.Products.Count);
        }
    }
}
