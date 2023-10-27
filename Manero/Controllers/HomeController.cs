﻿using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ActivePage = "Home";
        var products = new GridCollectionViewModel
        {
            Title = "Home",
            GridItems = (await _productService.GetAllAsync()).Take(4),
        };
        return View(products);
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
