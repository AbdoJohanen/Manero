using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.IdentityModel.Tokens;

namespace Manero.Helpers.Services.DataServices;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


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


    // Gets a list of ProductModel from repository
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