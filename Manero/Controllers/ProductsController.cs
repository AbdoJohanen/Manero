using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manero.Controllers
{
    public class ProductsController : Controller
    {
	
		private readonly ProductService _productService;

		public ProductsController(ProductService productService)
		{
			_productService = productService;
		}

        public async Task<IActionResult> Index(string id)
        {

            var productModel = await _productService.GetProductWithImagesAsync(id);
            
            return View(productModel);
        }
    }
}
