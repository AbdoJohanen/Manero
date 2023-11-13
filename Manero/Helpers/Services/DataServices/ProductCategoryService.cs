using Manero.Helpers.Repositories.DataRepositories;
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

    // Gets a list of ProductCategoryModel from repository
    public async Task<IEnumerable<ProductCategoryModel>> GetProductWithCategoriesAsync()
    {
        var productCategories = new List<ProductCategoryModel>();
        foreach (var item in await _productCategoryRepository.GetAllAsync())
            productCategories.Add(item);

        return productCategories;
    }

    // First gets list of ProductCategoryModel by articleNumber
    // Then removing the existing CategoryIds
    // Then creates new ProductCategoryModel by looping thru tags then return updated ProductCategories for product
    public async Task<IEnumerable<ProductCategoryModel>> UpdateProductCategoriesAsync(string articleNumber, List<int> categories)
    {
        var productCategories = new List<ProductCategoryModel>();
        var existingCategories = await _productCategoryRepository.GetAllAsync(x => x.ArticleNumber == articleNumber);

        if (existingCategories != null)
        {
            foreach (var category in existingCategories)
                await _productCategoryRepository.DeleteAsync(category);

            foreach (var categoryId in categories)
            {
                var updatedProductCategory = await _productCategoryRepository.AddAsync(new ProductCategoryModel { ArticleNumber = articleNumber, CategoryId = categoryId });
                productCategories.Add(updatedProductCategory);
            }

            return productCategories;
        }

        return null!;
    }
}

