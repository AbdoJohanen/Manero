﻿using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.AspNetCore.Http.HttpResults;
using Manero.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Manero.Helpers.Services.DataServices;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly ProductRepository _productRepo;
    private readonly ProductCategoryRepository _productCategoryRepo;
    private readonly ImageService _imageService;
    private readonly CategoryRepository _categoryRepository;
    private readonly TagService _tagService;
    private readonly TagRepository _tagRepo;
    private readonly ProductTagRepository _productTagRepo;

    public ProductRepository Repository { get; }

    public ProductService(ProductRepository productRepository, CategoryService categoryService, ProductRepository productRepo, ProductCategoryRepository productCategoryRepo, ImageService imageService, CategoryRepository categoryRepository, TagService tagService, TagRepository tagRepo, ProductTagRepository productTagRepo)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
        _productRepo = productRepo;
        _productCategoryRepo = productCategoryRepo;
        _imageService = imageService;
        _categoryRepository = categoryRepository;
        _tagService = tagService;
        _tagRepo = tagRepo;
        _productTagRepo = productTagRepo;
    }


    // Sends ProductModel from view to repository
    public async Task<ProductModel> CreateProductAsync(ProductModel model)
    {
        if (model != null)
            return await _productRepository.AddAsync(model);

        return null!;
    }


    // Finds specific ProductModel using a ProductModel with expression from repository
    public async Task<ProductModel> GetProductAsync(ProductModel product)
    {
        var _product = await _productRepository.GetAsync(x => x.ArticleNumber == product.ArticleNumber);
        return _product!;
    }

    // Finds specific ProductModel using a articleNumber with expression from repository
    public async Task<ProductModel> GetProductAsync(string articleNumber)
    {
        var _product = await _productRepository.GetAsync(x => x.ArticleNumber == articleNumber);
        return _product!;
    }


    //Gets a list of ProductModel from repository
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

    // Finds product (item) using articleNumber, then replaces values of that item with new values from product
    public async Task<ProductModel> UpdateProductAsync(ProductModel product, string articleNumber)
    {
        var item = await _productRepository.GetAsync(x => x.ArticleNumber == articleNumber);
        if (item != null)
        {
            product.ArticleNumber = item.ArticleNumber;

            item.ProductName = product.ProductName ?? item.ProductName;
            item.ProductDescription = product.ProductDescription ?? item.ProductDescription;
            item.ProductPrice = (product.ProductPrice != 0) ? product.ProductPrice : item.ProductPrice;
            item.ProductDiscount = product.ProductDiscount ?? item.ProductDiscount;

            return await _productRepository.UpdateAsync(item);
        }

        return null!;
    }



    //Get product with images based on article number
 
    public async Task<ProductModel> GetProductWithImagesAsync(string id)
    {
        var item = await _productRepo.GetAsync(x => x.ArticleNumber == id);

        if (item == null)
        {
            return null;
        }

        var productImages = await _imageService.GetAllImagesAsync(id);
        var images = await _imageService.GetAllImagesAsync();

        ProductModel productModel = item;

        var matchingImages = productImages
           .Select(pi => images.FirstOrDefault(img => img.Id == pi.Id));

        productModel.Images = matchingImages.ToList();

        return productModel;
    }

  



    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        var products = new List<ProductModel>();

        var items = await _productRepo.GetAllAsync();
        var productCategories = await _productCategoryRepo.GetAllAsync();
        var productImages = await _imageService.GetAllAsync();
        var productTags = await _productTagRepo.GetAllAsync();
      
        var categories = await _categoryService.GetAllCategoriesToModelAsync();
        var images = await _imageService.GetAllImagesAsync();
        var tags = await _tagService.GetAllTagsToModelAsync();

        foreach (var item in items)
        {           
            ProductModel productModel = item;

            var matchingCategories = productCategories
                .Where(productCategory => productCategory.ArticleNumber.ToString() == item.ArticleNumber)
                .Select(productCategory => categories.FirstOrDefault(Category => Category.Id == productCategory.CategoryId));

            productModel.Categories = matchingCategories.ToList()!;

            var matchingTags = productTags
                .Where(productTag => productTag.ArticleNumber.ToString() == item.ArticleNumber)
                .Select(productTag => tags.FirstOrDefault(Tag => Tag.Id == productTag.TagId));

            productModel.Tags = matchingTags.ToList()!;


            var matchingImages = productImages
               .Where(productImage => productImage.ProductArticleNumber.ToString() == item.ArticleNumber)
               .Select(productImage => images.FirstOrDefault(Image => Image.Id == productImage.Id));

            productModel.Images = matchingImages.ToList()!;

            products.Add(productModel);
        }

        return products;
    }

    // Takes article number from view and finds then sends to delete() to repository
    public async Task<bool> DeleteProductAsync(string articleNumber)
    {
        if (!articleNumber.IsNullOrEmpty())
        {
            var product = await _productRepository.GetAsync(x => x.ArticleNumber == articleNumber);
            if (product != null)
                return await _productRepository.DeleteAsync(product);
        }

        return false;
    }


    //Calculate product discount price
    private decimal? CalcProductPrice(ProductModel model)
    {
        if (model != null && model.ProductDiscount != null)
        {
            var discountPrice = model.ProductPrice * (model.ProductDiscount / 100);
            return discountPrice;
        }

        return model!.ProductPrice;
    }

    public async Task<IEnumerable<ProductModel>> GetFilteredProductsAsync(ProductFilterViewModel filters)
    {
        var productEntities = await _productRepository.GetFilteredProductsAsync(filters);

        // Convert the entities to DTOs and fetch images for each product
        var productModels = new List<ProductModel>();
        foreach (var productEntity in productEntities)
        {
            var productModel = (ProductModel)productEntity;

            // Fetch images for each product
            var images = await _imageService.GetAllImagesAsync(productEntity.ArticleNumber);
            productModel.Images = images.Select(img => (ImageModel)img).ToList();

            productModels.Add(productModel);
        }

        return productModels;
    }

}
