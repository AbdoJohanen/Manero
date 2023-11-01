using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductSizeService
{
    private readonly ProductSizeRepository _productSizeRepository;
    private readonly SizeService _sizeService;
    private readonly ProductService _productService;

    public ProductSizeService(ProductSizeRepository productSizeRepository, SizeService sizeService, ProductService productService)
    {
        _productSizeRepository = productSizeRepository;
        _sizeService = sizeService;
        _productService = productService;
    }



    // Takes a list of int (selected tags) and a ProductModel, than sends a new ProductTag to the repository
    public async Task AssociateSizesWithProductAsync(IEnumerable<SizeModel> selectedSizes, ProductModel product)
    {
        if (selectedSizes.Any() && product != null)
        {
            var articleNumber = product.ArticleNumber;
            foreach (var size in selectedSizes)
            {
                await _productSizeRepository.AddAsync(new ProductSizeModel
                {
                    ArticleNumber = articleNumber,
                    Product = product,
                    SizeId = size.Id,
                    Size = size
                });
            }
        }
    }

    // Gets a list of ProductTagModel from repository
    public async Task<IEnumerable<ProductSizeModel>> GetProductWithSizesAsync()
    {
        var productSizes = new List<ProductSizeModel>();
        foreach (var item in await _productSizeRepository.GetAllAsync())
            productSizes.Add(item);

        return productSizes;
    }

    // First gets list of ProductSizeModel by articleNumber
    // Then removing the existing SizeIds
    // Then creates new ProductSizeModel by looping thru sizes then return updated ProductSizes for product
    public async Task<IEnumerable<ProductSizeModel>> UpdateProductSizesAsync(string articleNumber, List<int> sizes)
    {
        var productSizes = new List<ProductSizeModel>();
        var existingSizes = await _productSizeRepository.GetAllAsync(x => x.ArticleNumber == articleNumber);

        if (existingSizes != null)
        {
            foreach (var size in existingSizes)
                await _productSizeRepository.DeleteAsync(size);

            foreach (var sizeId in sizes)
            {
                var updatedProductSize = await _productSizeRepository.AddAsync(new ProductSizeModel { ArticleNumber = articleNumber, SizeId = sizeId });
                productSizes.Add(updatedProductSize);
            }

            return productSizes;
        }

        return null!;
    }

}
