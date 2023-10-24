using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
{
    private readonly ProductService _productService;
    private readonly TagService _tagService;
    private readonly ProductTagService _productTagService;
    private readonly CategoryService _categoryService;
    private readonly ProductCategoryService _productCategoryService;

    public BackOfficeController(ProductService productService, TagService tagService, ProductTagService productTagService, CategoryService categoryService, ProductCategoryService productCategoryService)
    {
        _productService = productService;
        _tagService = tagService;
        _productTagService = productTagService;
        _categoryService = categoryService;
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {

        var viewModel = new CreateProductFormViewModel();

        foreach (var tag in await _tagService.GetAllTagsAsync())
        {
            viewModel.Tags.Add(tag);
        }


        foreach (var category in await _categoryService.GetAllCategoriesAsync())
        {
            viewModel.Categories.Add(category);
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductFormViewModel viewModel)
    {

        foreach (var tag in await _tagService.GetAllTagsAsync())
        {
            viewModel.Tags.Add(tag);
        }


        foreach (var category in await _categoryService.GetAllCategoriesAsync())
        {
            viewModel.Categories.Add(category);
        }


        if (ModelState.IsValid)
        {
            var selectedTags = viewModel.SelectedTags;
            var selectedCategories = viewModel.SelectedCategories;

            var tags = await _tagService.GetTagsAsync(selectedTags);
            var categories = await _categoryService.GetCategoriesAsync(selectedCategories);
            var product = await _productService.CreateProductAsync(viewModel);

            await _productTagService.AssociateTagsWithProductAsync(tags, product);
            await _productCategoryService.AssociateCategoriesWithProductAsync(categories, product);

            return RedirectToAction("Index");
        }

        ModelState.AddModelError("Model", "Something went wrong! Could not create a product");
        return View(viewModel);
    }
}