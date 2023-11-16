using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductService _productService;
        private readonly ImageService _imageService;

        public ShopController(ProductService productService, ImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        [Route("Shop")]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _productService.GetAllAsync();

            ViewBag.ActivePage = "Shop";
            var viewModel = new ShopViewModel
            {
                Items = allProducts
            };
            return View(viewModel);
        }
    }
}
