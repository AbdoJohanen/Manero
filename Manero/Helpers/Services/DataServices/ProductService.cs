using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.IdentityModel.Tokens;

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

    //public ProductService() 
    //{

    //}


    // Sends ProductModel from view to repository
    public async Task<ProductModel> CreateProductAsync(ProductModel model)
    {
        if (model != null)
            return await _productRepository.AddAsync(model);

        return null!;
    }


    // Finds specific ProductModel with expression from repository
    public async Task<ProductModel> GetProductAsync(ProductModel product)
    {
        var _product = await _productRepository.GetAsync(x => x.ArticleNumber == product.ArticleNumber);
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
    public async Task<ProductModel> GetProductWithImagesAsync(string id)
    {
        // Hämta produkten med det angivna artikelnumret
        var item = await _productRepo.GetAsync(x => x.ArticleNumber == id);

        if (item == null)
        {
            return null; // Produkten finns inte
        }

        // Hämta alla bilder som matchar den aktuella produkten
        var productImages = await _imageService.GetAllImagesAsync(id);
        var images = await _imageService.GetAllImagesAsync();

        // Skapa en produktmodell baserad på produktinformationen
        ProductModel productModel = item;

        // Hitta bilderna som matchar den aktuella produkten
        var matchingImages = productImages
           .Select(pi => images.FirstOrDefault(img => img.Id == pi.Id));

        // Lägg till bilderna i produktmodellen
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

        // Hämta kategorier med den nya metoden
        var categories = await _categoryService.GetAllCategoriesToModelAsync();
        var images = await _imageService.GetAllImagesAsync();
        var tags = await _tagService.GetAllTagsToModelAsync();

        foreach (var item in items)
        {
            // Skapa en produktmodell baserad på produktinformationen
            ProductModel productModel = item;

            // Hitta produktkategorierna som matchar den aktuella produkten
            var matchingCategories = productCategories
                .Where(pc => pc.ArticleNumber.ToString() == item.ArticleNumber)
                .Select(pc => categories.FirstOrDefault(c => c.Id == pc.CategoryId));

            // Lägg till kategorierna i produktmodellen
            productModel.Categories = matchingCategories.ToList();

            var matchingTags = productTags
                .Where(pc => pc.ArticleNumber.ToString() == item.ArticleNumber)
                .Select(pc => tags.FirstOrDefault(c => c.Id == pc.TagId));

            // Lägg till kategorierna i produktmodellen
            productModel.Tags = matchingTags.ToList();

            // Hitta bilderna som matchar den aktuella produkten

            var matchingImages = productImages
               .Where(pc => pc.ProductArticleNumber.ToString() == item.ArticleNumber)
               .Select(pc => images.FirstOrDefault(c => c.Id == pc.Id));

            // Lägg till bilderna i produktmodellen
            productModel.Images = matchingImages.ToList();

            // Lägg till produkten i listan av produkter
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
}

//var matchingImages = productImages.Where(image => image.Id == item.ArticleNumber);
//var matchingImages = productImages.Where(image => image.Id.ToString() == item.ArticleNumber);
//var matchingImages = images.Where(image => image.Id.ToString() == item.ArticleNumber);