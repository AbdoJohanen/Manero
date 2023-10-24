using Manero.Helpers.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ProductService _productService;

        //public HomeController(ProductService productService)
        //{
        //    _productService = productService;
        //}

        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Home";

            //var GridItems = (await _productService.GetAllAsync()).Take(4);
            return View();
        }

    }
}
