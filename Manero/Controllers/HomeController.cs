using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        var allProducts = await _productService.GetAllAsync();

        // Filtrering för bäst säljande produkter
        var bestSellingProducts = allProducts
            .Where(product => product.Tags.Any(tag => tag.Tag.Contains("Best Sellers")))
            .Take(3)
            .ToList();

        // Filtrering för utvalda produkter
        var featuredProducts = allProducts
            .Where(product => product.Tags.Any(tag => tag.Tag.Contains("Featured Products")))
            .Take(4)
            .ToList();

        ViewBag.ActivePage = "Home";
        var viewModel = new HomeIndexViewModel
        {

            Categories = await _categoryService.GetAllCategoriesToModelAsync(),

            Featured = new GridCollectionViewModel
            {
                Title = "Featured products",
                GridItems = featuredProducts,
            },
            BestSelling = new BestSellingViewModel
            {
                Title = "Best Sellers",
                GridItems = bestSellingProducts,
            }
        };
        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
