using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
{
    private readonly ProductService _productService;
    private readonly TagService _tagService;

    public BackOfficeController(ProductService productService, TagService tagService)
    {
        _productService = productService;
        _tagService = tagService;
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
            await _productService.CreateProductAsync(viewModel);
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("Model", "Something went wrong! Could not register user");
        return View(viewModel);
    }
}




/*
[HttpPost]
public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel)
{
    if (ModelState.IsValid)
    {
        var result = await productService.SaveProductAsync(viewModel);
        if (result)
            return RedirectToAction("ManageProducts");

        ModelState.AddModelError("Model", "Something went wrong! Could not create the product");
        return View(viewModel);
    }

    return View(viewModel);
}
*/