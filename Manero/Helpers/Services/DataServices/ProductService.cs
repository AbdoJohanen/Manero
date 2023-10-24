﻿using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Services.DataServices;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductModel> CreateProductAsync(ProductModel model)
    {
        if (model != null)
            return await _productRepository.AddAsync(new ProductModel
            {
                ArticleNumber = model.ArticleNumber,
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                ProductPrice = model.ProductPrice,
                ProductDiscount = model.ProductDiscount
            });

        return null!;
    }

    public async Task<ProductModel> GetProductAsync(ProductModel product)
    {
        var _product = await _productRepository.GetAsync(x => x.ArticleNumber == product.ArticleNumber);
        return _product!;
    }

    public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
    {
        var items = await _productRepository.GetAllAsync();
        if (items != null)
        {
            var products = new List<ProductModel>();
            foreach (var item in items)
                products.Add(item);

            return products;
        }

        return null!;
    }




    // Calculate product discount price
    /*
    private decimal? CalcProductPrice(ProductModel model)
    {
        if (model != null && model.ProductDiscount != null)
        {
            var discountPrice = model.ProductPrice * (model.ProductDiscount / 100);
            return discountPrice;
        }
        
        return model!.ProductPrice;
    }
    */
}




// OLD REFRENCE
/*
public class ProductService
{


private readonly CategoryService _categoryService;
private readonly ProductRepository _productRepo;
private readonly ProductCategoryRepository _productCategoryRepo;
private readonly ImageService _imageService;

public ProductService(CategoryService categoryService, ImageService imageService, ProductCategoryRepository productCategoryRepo, ProductRepository productRepo)
{
    _categoryService = categoryService;
    _imageService = imageService;
    _productCategoryRepo = productCategoryRepo;
    _productRepo = productRepo;
}

public async Task<bool> SaveProductAsync(CreateProductViewModel model)
{
    if (model != null)
    {

        var _findProduct = await _productRepo.GetAsync(x => x.Name == model.Name);
        if (_findProduct == null)
        {
            // If product dosent exists, make a new
            ProductEntity _product = model;
            _product = await _productRepo.AddAsync(_product);

            // Handel Images
            if (model.Image != null)
            {
                await _imageService.SaveProductImageAsync(_product, model.Image);
            }

            // Handle Categories

            foreach (var category in model.Categories)
            {
                if (category.isActive == true)
                {
                    await _productCategoryRepo.AddAsync(new ProductCategoryEntity { productId = _product.Id, categoryId = category.Id });
                }
            }

            return true;
        }
    }
    return false;
}
}

*/