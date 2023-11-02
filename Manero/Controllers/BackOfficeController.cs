using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
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

    public BackOfficeController(ProductService productService, TagService tagService, ProductTagService productTagService, CategoryService categoryService, ProductCategoryService productCategoryService, SizeService sizeService, ProductSizeService productSizeService, ImageService imageService, ColorService colorService, ProductColorService productColorService)
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
    public async Task<IActionResult> Index(BackOfficeViewModel viewModel)
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
            foreach(var size in await _productSizeService.GetProductWithSizesAsync())
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
            foreach (var image in await _imageService.GetAllProductImagesAsync(product.ArticleNumber))
                if (image != null)
                    product.Images.Add(image);

            // Adds ProductModel to list of ProductModel in View Model
            viewModel.Products.Add(product);
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var viewModel = new CreateProductFormViewModel();

        await PopulateViewModelAsync(viewModel);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Sets the selected tags to list of tags id
            var selectedTags = viewModel.SelectedTags;
            var selectedCategories = viewModel.SelectedCategories;
            var selectedSizes = viewModel.SelectedSizes;
            var selectedColors = viewModel.SelectedColors;

            // Gets the tags, categories, sizes from services
            var tags = await _tagService.GetTagsAsync(selectedTags);
            var categories = await _categoryService.GetCategoriesAsync(selectedCategories);
            var sizes = await _sizeService.GetSizesAsync(selectedSizes);
            var colors = await _colorService.GetColorsAsync(selectedColors);
            
            var product = await _productService.CreateProductAsync(viewModel);

            if (product != null)
            {
                // Checks if Images is not null
                if (viewModel.Images != null)
                {
                    var isMainImage = false;
                    foreach (var image in viewModel.Images)
                    {
                        if (viewModel.MainImageFileName == image.FileName)
                        {
                            isMainImage = true;
                        }
                        await _imageService.SaveProductImageAsync(product, image, isMainImage);
                        isMainImage = false;
                    }
                }

                // Associates tags, sizes and categories with the product
                await _productTagService.AssociateTagsWithProductAsync(tags, product);
                await _productCategoryService.AssociateCategoriesWithProductAsync(categories, product);
                await _productSizeService.AssociateSizesWithProductAsync(sizes, product);
                await _productColorService.AssociateColorsWithProductAsync(colors, product);
            }
            return RedirectToAction("Index");
        }
        
        // If ModelState is not valid return view with errors
        await PopulateViewModelAsync(viewModel);
        ModelState.AddModelError("Model", "Something went wrong! Could not create a product");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(string articleNumber, string errorMessage)
    {
        var viewModel = new UpdateProductFormViewModel();
        if (!string.IsNullOrEmpty(articleNumber))
        {
            // Gets the product the admin selected for updating
            viewModel.Product = await _productService.GetProductAsync(articleNumber);

            // Gets all product tags, catgeories, colors and sizes 
            var productTags = await _productTagService.GetProductWithTagsAsync();
            var productCategories = await _productCategoryService.GetProductWithCategoriesAsync();
            var productColors = await _productColorService.GetProductWithColorsAsync();
            var productSizes = await _productSizeService.GetProductWithSizesAsync();
            
            // Uses select to add current tags, categories, colors by selecting with id
            viewModel.CurrentTags = productTags.Select(tag => tag.TagId).ToList();
            viewModel.CurrentCategories = productCategories.Select(category => category.CategoryId).ToList();
            viewModel.CurrentColors = productColors.Select(color => color.ColorId).ToList();
            viewModel.CurrentSizes = productSizes.Select(size => size.SizeId).ToList();
            viewModel.CurrentImages = await _imageService.GetAllProductImagesAsync(articleNumber);

            // Populates the list of Tags in viewModel
            foreach (var tag in await _tagService.GetAllTagsAsync())
                viewModel.Tags.Add(tag);

            // Populates the list of Categories in viewModel
            foreach (var category in await _categoryService.GetAllCategoriesAsync())
                viewModel.Categories.Add(category);

            // Populates the list of Colors in viewModel
            foreach (var color in await _colorService.GetAllColorsAsync())
                viewModel.Colors.Add(color);

            // Populates the list of Sizes in viewModel
            foreach (var size in await _sizeService.GetAllSizesAsync())
                viewModel.Sizes.Add(size);
        }

        if (!string.IsNullOrEmpty(errorMessage))
            ModelState.AddModelError("", errorMessage);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductFormViewModel viewModel, string articleNumber)
    {
        // If UpdateProductAsync is not null then redirect to Backoffice Index
        var product = await _productService.UpdateProductAsync(viewModel, articleNumber);
        if (product != null)
        {
            // Updates the ProductTag table with new information of tags but the same articleNumber
            if (await _productTagService.UpdateProductTagsAsync(articleNumber, viewModel.SelectedTags!) != null)
            
            // Updates the productCategory table with new information of categories but the same articleNumber
            if (await _productCategoryService.UpdateProductCategoriesAsync(articleNumber, viewModel.SelectedCategories!) != null)

            // Updates the productColor table with new information of colors but the same articleNumber
            if (await _productColorService.UpdateProductColorsAsync(articleNumber, viewModel.SelectedColors!) != null)

            // updates the productSize table with new information of sizes but the same articleNumber
            if (await _productSizeService.UpdateProductSizesAsync(articleNumber, viewModel.SelectedSizes!) != null)

            return RedirectToAction("Index");
        }

        // If something failed with update return View with error message
        return RedirectToAction("UpdateProduct", new { articleNumber, errorMessage = "Could not update product!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(string articleNumber)
    {
        // Checks if the product is deleted then return to page
        if (await _productService.DeleteProductAsync(articleNumber))
            return RedirectToAction("Index");

        // If delete fail, display error
        ModelState.AddModelError("", "Something went wrong, could not delete!");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(string imageId, string articleNumber)
    {
        if (await _imageService.DeleteImageAsync(imageId))
            return RedirectToAction("UpdateProduct", new { articleNumber });

        return RedirectToAction("UpdateProduct", new { articleNumber, errorMessage = "Could not remove image..." });
    }

    private async Task PopulateViewModelAsync(CreateProductFormViewModel viewModel)
    {
        // Gets all tags
        viewModel.Tags = await _tagService.GetAllTagsAsync();

        // Gets all categories
        viewModel.Categories = await _categoryService.GetAllCategoriesAsync();

        // Gets all sizes
        viewModel.Sizes = await _sizeService.GetAllSizesAsync();

        // Gets all sizes
        viewModel.Colors = await _colorService.GetAllColorsAsync();
    }
}