using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
{
    private readonly ProductService _productService;
    private readonly TagService _tagService;
    private readonly ProductTagService _productTagService;

    public BackOfficeController(ProductService productService, TagService tagService, ProductTagService productTagService)
    {
        _productService = productService;
        _tagService = tagService;
        _productTagService = productTagService;
    }

    [HttpGet]
    public async Task<IActionResult> Index() 
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateProductFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var selectedTags = viewModel.SelectedTags;

            var tags = await _tagService.GetTagsAsync(selectedTags);
            var product = await _productService.CreateProductAsync(viewModel);

            await _productTagService.AssociateTagsWithProductAsync(tags, product);
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("Model", "Something went wrong! Could not create a product");
        return View(viewModel);
    }
}