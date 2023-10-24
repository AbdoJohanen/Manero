using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
{
    private readonly ProductService _productService;
    private readonly TagService _tagService;
    private readonly ProductTagService _productTagService;
    private readonly ImageService _imageService;

    public BackOfficeController(ProductService productService, TagService tagService, ProductTagService productTagService, ImageService imageService)
    {
        _productService = productService;
        _tagService = tagService;
        _productTagService = productTagService;
        _imageService = imageService;
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
            viewModel.Tags.Add(tag);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var selectedTags = viewModel.SelectedTags;

            var tags = await _tagService.GetTagsAsync(selectedTags);
            var product = await _productService.CreateProductAsync(viewModel);

            await _productTagService.AssociateTagsWithProductAsync(tags, product);

            if (viewModel.Images != null)
            {
                foreach (var image in viewModel.Images)
                {
                    await _imageService.SaveProductImageAsync(product, image);

                }
            }

            return RedirectToAction("Index");
        }

        ModelState.AddModelError("Model", "Something went wrong! Could not create a product");
        return View(viewModel);
    }
}