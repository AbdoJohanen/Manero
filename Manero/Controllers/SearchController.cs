using Manero.Helpers.Services.DataServices;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class SearchController : Controller
{
    private readonly CategoryService _categoryService;

    public SearchController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ActivePage = "Search";

        var viewModel = new SearchIndexViewModel
        {
            Categories = await _categoryService.GetAllCategoriesToModelAsync()
        };


        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
