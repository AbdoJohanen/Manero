using Manero.Helpers.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class ProductsController : Controller
{
	
	private readonly ProductService _productService;

	public ProductsController(ProductService productService)
	{
		_productService = productService;
	}

    public async Task<IActionResult> ProductDetails(string id)
    {

        var productModel = await _productService.GetProductWithImagesAsync(id);
        
        return View(productModel);
    }
}
