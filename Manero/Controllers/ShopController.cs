using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductService _productService;
        private readonly TagService _tagService;
        private readonly ProductTagService _productTagService;
        private readonly ImageService _imageService;
        private readonly CategoryService _categoryService;
        private readonly ProductCategoryService _productCategoryService;
        private readonly SizeService _sizeService;
        private readonly ProductSizeService _productSizeService;
        private readonly ColorService _colorService;
        private readonly ProductColorService _productColorService;

        public ShopController(ProductService productService, TagService tagService, ProductTagService productTagService, CategoryService categoryService, ProductCategoryService productCategoryService, SizeService sizeService, ProductSizeService productSizeService, ImageService imageService, ColorService colorService, ProductColorService productColorService)
        {
            _productService = productService;
            _tagService = tagService;
            _productTagService = productTagService;
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
            _imageService = imageService;
            _sizeService = sizeService;
            _productSizeService = productSizeService;
            _colorService = colorService;
            _productColorService = productColorService;
        }

        [HttpGet]
        [Route("shop")]
        public async Task<IActionResult> Index(ShopViewModel viewModel)
        {
            // Loops thru all products
            foreach (var product in await _productService.GetAllProductsAsync())
            {
                // Loops thru all productTags
                foreach (var tag in await _productTagService.GetProductWithTagsAsync())
                {
                    // If a product tag article number is the same as one of the products article number
                    // Then find and add that TagModel to the list of TagModel in ProductModel
                    if (tag.ArticleNumber == product.ArticleNumber)
                        product.Tags.Add(await _tagService.GetTagAsync(tag.TagId));
                }

                // Loops thru all productCategories
                foreach (var category in await _productCategoryService.GetProductWithCategoriesAsync())
                {
                    // If a product category articlenumber is the same as on of the products article number
                    // Then find and add that CategoryModel to the list of CategoryModel in ProductModel
                    if (category.ArticleNumber == product.ArticleNumber)
                        product.Categories.Add(await _categoryService.GetCategoryAsync(category.CategoryId));
                }

                // Loops thru all productSizes
                foreach (var size in await _productSizeService.GetProductWithSizesAsync())
                {
                    // If a product size articlenumber is the same as one of the products article number
                    // Then find and add that SizeModel to the list of SizeModel in ProductModel
                    if (size.ArticleNumber == product.ArticleNumber)
                        product.Sizes.Add(await _sizeService.GetSizeAsync(size.SizeId));
                }

                // Loops thru all productColors
                foreach (var color in await _productColorService.GetProductWithColorsAsync())
                {
                    // If a product color articlenumber is the same as one of the products article number
                    // Then find and add that ColorModel to the list of ColorModel in ProductModel
                    if (color.ArticleNumber == product.ArticleNumber)
                        product.Colors.Add(await _colorService.GetColorAsync(color.ColorId));
                }

                // Gets Main Image and adds the image to the ProductModel
                product.Images = new List<ImageModel>();
                product.Images.Add(await _imageService.GetMainImageAsync(product.ArticleNumber));

                // Adds ProductModel to list of ProductModel in View Model
                viewModel.Products.Add(product);
            }

            return View(viewModel);
        }
    }
}
