﻿using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class BackOfficeController : Controller
{
    private readonly ProductService _productService;
    private readonly TagService _tagService;
    private readonly ProductTagService _productTagService;
    private readonly CategoryService _categoryService;
    private readonly ProductCategoryService _productCategoryService;

    public BackOfficeController(ProductService productService, TagService tagService, ProductTagService productTagService, CategoryService categoryService, ProductCategoryService productCategoryService)
    {
        _productService = productService;
        _tagService = tagService;
        _productTagService = productTagService;
        _categoryService = categoryService;
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(BackOfficeViewModel viewModel)
    {
        // Gets list of ProductTagModel
        var productTags = await _productTagService.GetProductWithTagsAsync();

        var categoryTags = await _productCategoryService.GetProductWithCategoriesAsync();

        // Gets all products
        var products = await _productService.GetAllProductsAsync();

        // Loops thru all products
        foreach (var product in products)
        {
            // Loops thru all productTags
            foreach (var tag in productTags)
            {
                // If a product tag article number is the same as one of the products article number
                // Then find and add that TagModel to the list of TagModel in ProductModel
                if (tag.ArticleNumber == product.ArticleNumber)
                    product.Tags.Add(await _tagService.GetTagAsync(tag.TagId));
            }

            // Loops thru all categoryTags
            foreach (var category in categoryTags)
            {
                // If a product category articlenumber is the same as on of the products article number
                // Then find and add that CategoryModel to the list of CategoryModel in ProductModel
                if (category.ArticleNumber == product.ArticleNumber)
                    product.Categories.Add(await _categoryService.GetCategoryAsync(category.CategoryId));
            }

            // Adds ProductModel to list of ProductModel in View Model
            viewModel.Products.Add(product);
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var viewModel = new CreateProductFormViewModel();

        // Gets all tags and adds them to the ViewModel list of TagModel
        foreach (var tag in await _tagService.GetAllTagsAsync())
            viewModel.Tags.Add(tag);

        // Gets all categories and adds them to the ViewModel list of CategoryModel
        foreach (var category in await _categoryService.GetAllCategoriesAsync())
            viewModel.Categories.Add(category);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Sets the selected tags to list of tags id
            var selectedTags = viewModel.SelectedTags;
            var selectedCategories = viewModel.SelectedCategories;

            // Gets the tags from database using the list selected tags
            var tags = await _tagService.GetTagsAsync(selectedTags);
            var categories = await _categoryService.GetCategoriesAsync(selectedCategories);
            var product = await _productService.CreateProductAsync(viewModel);

            // Associates tags with the product that was created
            await _productTagService.AssociateTagsWithProductAsync(tags, product);
            await _productCategoryService.AssociateCategoriesWithProductAsync(categories, product);

            return RedirectToAction("Index");
        }

        // If ModelState is invalid load all tags again
        foreach (var tag in await _tagService.GetAllTagsAsync())
            viewModel.Tags.Add(tag);

        // If ModelState is invalid load all tags again
        foreach (var category in await _categoryService.GetAllCategoriesAsync())
            viewModel.Categories.Add(category);

        // Shows error if ModelState is not valid
        ModelState.AddModelError("Model", "Something went wrong! Could not create a product");
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(string articleNumber)
    {
        // Checks if the product is deleted then return to page
        if (await _productService.DeleteProductAsync(articleNumber))
            return RedirectToAction("Index");

        // If delete fail, display error
        ModelState.AddModelError("", "Something went wrong, could not delete!");
        return RedirectToAction("Index");
    }
}