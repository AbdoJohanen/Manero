using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Services.DataServices;

public class ProductService
{


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