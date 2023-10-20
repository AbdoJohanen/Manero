using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
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

    public async Task<IActionResult> Index(CreateProductFormViewModel viewModel) 
    {
        var tags = await _tagService.GetAllTagsAsync(viewModel.Tags);
        foreach (var tag in tags)
            viewModel.Tags.Add(tag);


        return View();
    }
}
