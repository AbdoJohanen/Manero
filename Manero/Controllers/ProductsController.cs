using Manero.Helpers.Services.DataServices;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class ProductsController : Controller
{
	
	private readonly ProductService _productService;
    private readonly SizeService _sizeService;
    private readonly TagService _tagService;
    private readonly CategoryService _categoryService;
    private readonly ColorService _colorService;

    public ProductsController(ProductService productService, SizeService sizeService, TagService tagService, CategoryService categoryService, ColorService colorService)
    {
        _productService = productService;
        _sizeService = sizeService;
        _tagService = tagService;
        _categoryService = categoryService;
        _colorService = colorService;
    }

    // This action responds to both GET and POST requests.
    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> Index([FromQuery] ProductFilterViewModel filter)
    {
        // If the action is called via GET with query parameters, the filter will be populated accordingly.
        // If it is a POST request (form submission from the partial), the filter will also contain the appropriate data.

        // Fetch the available filter options from the database
        filter.AvailableSizes = (List<Models.DTO.SizeModel>)await _sizeService.GetAllSizesAsync();
        filter.AvailableColors = (List<Models.DTO.ColorModel>)await _colorService.GetAllColorsAsync();
        filter.AvailableTags = (List<Models.DTO.TagModel>)await _tagService.GetAllTagsAsync();
        filter.AvailableCategories = (List<Models.DTO.CategoryModel>)await _categoryService.GetAllCategoriesAsync();

        // If the filter is empty, initialize it with the available options
        filter.Sizes = filter.Sizes ?? Array.Empty<string>();
        filter.Categories = filter.Categories ?? Array.Empty<string>();
        filter.Tags = filter.Tags ?? Array.Empty<string>();
        filter.Colors = filter.Colors ?? Array.Empty<string>();
        filter.MaxPrice = filter.MaxPrice;
        filter.MinPrice = filter.MinPrice;

        // Fetch the filtered products based on the provided filter
        var filteredProducts = await _productService.GetFilteredProductsAsync(filter);

        // Construct the view model for the page
        var viewModel = new ProductsViewModel
        {
            Products = filteredProducts.ToList(),
            CurrentFilter = "Filtered Products", // You can customize this based on the filter values
            ProductFilters = filter
        };

        // Render the view with the view model
        return View(viewModel);
    }

    public async Task<IActionResult> ProductDetails(string id)
    {

        var productModel = await _productService.GetProductWithImagesAsync(id);
        
        return View(productModel);
    }
}
