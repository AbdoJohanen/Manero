using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Services.DataServices;

public class ProductService
{
    private readonly CategoryService _categoryService;
    private readonly ProductRepository _productRepo;
    private readonly ProductCategoryRepository _productCategoryRepo;
    private readonly ImageService _imageService;
    private readonly CategoryRepository _categoryRepository;

    public ProductService(CategoryService categoryService, ProductRepository productRepo, ProductCategoryRepository productCategoryRepo, ImageService imageService, CategoryRepository categoryRepository)
    {
        _categoryService = categoryService;
        _productRepo = productRepo;
        _productCategoryRepo = productCategoryRepo;
        _imageService = imageService;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<(ProductEntity Product, List<ImageEntity> Images)>> GetAllAsync()
    {
        var productsWithImages = new List<(ProductEntity, List<ImageEntity>)>();

        var items = await _productRepo.GetAllAsync();
        var productCategories = await _productCategoryRepo.GetAllAsync();
        var categories = await _categoryRepository.GetAllAsync();
        var productImages = await _imageService.GetAllAsync();

        foreach (var item in items)
        {
            var productEntity = item;
            var productCategoryList = new List<CategoryEntity>();
            var productImageList = new List<ImageEntity>();

            // Find categories and images associated with the product
            foreach (var productCategory in productCategories.Where(pc => pc.ArticleNumber == item.ArticleNumber))
            {
                var category = categories.FirstOrDefault(c => c.Id == productCategory.CategoryId);
                if (category != null)
                {
                    productCategoryList.Add(new CategoryEntity
                    {
                        Id = category.Id,
                        Category = category.Category
                    });
                }
            }

            foreach (var productImage in productImages.Where(pi => pi.ArticleNumber == item.ArticleNumber))
            {
                productImageList.Add(new ImageEntity
                {
                    Id = productImage.Id,
                    ImageUrl = productImage.ImageUrl
                });
            }

            productsWithImages.Add((productEntity, productImageList));
        }

        return productsWithImages;
    }

    //public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    //{
    //    var products = new List<ProductEntity>();
    //    var productCategoriesDict = new Dictionary<int, List<CategoryEntity>>();
    //    var productImagesDict = new Dictionary<int, List<ImageEntity>>();

    //    var items = await _productRepo.GetAllAsync();
    //    var productCategories = await _productCategoryRepo.GetAllAsync();
    //    var categories = await _categoryRepository.GetAllAsync();
    //    var productImages = await _imageService.GetAllAsync();

    //    foreach (var item in items)
    //    {
    //        var productEntity = item;
    //        var productCategoryList = new List<CategoryEntity>();
    //        var productImageList = new List<ImageEntity>();

    //        // Find categories and images associated with the product
    //        foreach (var productCategory in productCategories.Where(pc => pc.ArticleNumber == item.ArticleNumber))
    //        {
    //            var category = categories.FirstOrDefault(c => c.Id == productCategory.CategoryId);
    //            if (category != null)
    //            {
    //                productCategoryList.Add(new CategoryEntity
    //                {
    //                    Id = category.Id,
    //                    Category = category.Category
    //                });
    //            }
    //        }

    //        foreach (var productImage in productImages.Where(pi => pi.Id == item.ArticleNumber))
    //        {
    //            productImageList.Add(new ImageEntity
    //            {
    //                Id = productImage.Id,
    //                ImageUrl = productImage.ImageUrl
    //            });
    //        }

    //        // Add the product and its associated categories and images
    //        products.Add(productEntity);
    //        productCategoriesDict[productEntity.ArticleNumber] = productCategoryList;
    //        productImagesDict[productEntity.ArticleNumber] = productImageList;
    //    }

    //    // Now you have a dictionary of product IDs to their associated categories and images
    //    // You can access them as needed for each product.

    //    return products;
    //}


    //public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    //{
    //    var products = new List<ProductEntity>();
    //    var categoriesEntities = new List<CategoryEntity>();
    //    var images = new List<ImageEntity>();

    //    var items = await _productRepo.GetAllAsync();
    //    var productCategories = await _productCategoryRepo.GetAllAsync();
    //    var categories = await _categoryRepository.GetAllAsync();
    //    var productImages = await _imageService.GetAllAsync();


    //    foreach (var item in items)
    //    {
    //        ProductEntity productEntity = item;
    //        foreach (var _item in productCategories)
    //        {
    //            foreach (var category in categories)
    //            {
    //                var categoryEntity = new CategoryEntity
    //                {
    //                    Category = category.Category,
    //                    Id = category.Id,
    //                };
    //                categoriesEntities.Add(categoryEntity);
    //            }
    //        }
    //        products.Add(productEntity);

    //        foreach(var image in productImages)
    //        {
    //            var imageEntity = new ImageEntity
    //            {
    //                Id = image.Id,
    //                ImageUrl = image.ImageUrl
    //            };
    //            images.Add(imageEntity);
    //        }

    //    }


    //    return products;
    //}
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