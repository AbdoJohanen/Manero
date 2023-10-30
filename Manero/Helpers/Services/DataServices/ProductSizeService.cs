using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductSizeService
{
    private readonly ProductSizeRepository _productSizeRepository;



    public ProductSizeService(ProductSizeRepository productSizeRepository)
    {
        _productSizeRepository = productSizeRepository;

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

}
