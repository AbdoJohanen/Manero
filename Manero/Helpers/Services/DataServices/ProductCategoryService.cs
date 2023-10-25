﻿using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductCategoryService
{
    private readonly ProductCategoryRepository _productCategoryRepository;

    public ProductCategoryService(ProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }
    public async Task AssociateCategoriesWithProductAsync(IEnumerable<CategoryModel> selectedCategories, ProductModel product)
    {


        if (selectedCategories.Any() && product != null)
        {
            var articleNumber = product.ArticleNumber;
            foreach (var category in selectedCategories)
            {
                await _productCategoryRepository.AddAsync(new ProductCategoryModel
                {
                 ArticleNumber = articleNumber,
                 Category = category,
                 Product = product,
                 CategoryId = category.Id

                });
            }
        }

    }
}

