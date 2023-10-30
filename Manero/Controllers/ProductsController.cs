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

		public async Task<IActionResult> Index (ProductModel product)
        {
	/*		var _product = await _productService.GetProductAsync(product);

			if (_product == null)
			{
				
			}

			var viewModel = new ProductDetailsViewModel
			{
*//*				ArticleNumber = product.ArticleNumber,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				ProductPrice = product.ProductPrice,
				ProductDiscount = product.ProductDiscount*//*
			};
*/
			return View(/*viewModel*/);
        }

    }
}
