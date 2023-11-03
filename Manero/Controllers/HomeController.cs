using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class HomeController : Controller
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public HomeController(ProductService productService, CategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ActivePage = "Home";
        var viewModel = new HomeIndexViewModel
        {
            Categories = await _categoryService.GetAllCategoriesToModelAsync(),
            Featured = new GridCollectionViewModel
            {
                Title = "Featured products",
                GridItems = (await _productService.GetAllAsync()).Take(4),
            },
            BestSelling = new BestSellingViewModel
            {
                GridItems = (await _productService.GetAllAsync()).Take(3),
            }
        };
        //var products = new GridCollectionViewModel
        //{
        //    Title = "Home",
        //    GridItems = (await _productService.GetAllAsync()).Take(4),
        //};
        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
