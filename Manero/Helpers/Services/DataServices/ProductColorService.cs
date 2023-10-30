using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductColorService
{
    private readonly ProductColorRepository _productColorRepository;

    public ProductColorService(ProductColorRepository productColorRepository)
    {
        _productColorRepository = productColorRepository;
    }

    public async Task AssociateColorsWithProductAsync(IEnumerable<ColorModel> selectedColors, ProductModel product)
    {
        if (selectedColors.Any() && product != null)
        {
            var articleNumber = product.ArticleNumber;
            foreach (var color in selectedColors)
            {
                await _productColorRepository.AddAsync(new ProductColorModel
                {
                    ArticleNumber = articleNumber,
                    ColorId = color.Id,
                });
            }
        }
    }

    // Gets a list of ProductCategoryModel from repository
    public async Task<IEnumerable<ProductColorModel>> GetProductWithColorsAsync()
    {
        var productColors = new List<ProductColorModel>();
        foreach (var item in await _productColorRepository.GetAllAsync())
            productColors.Add(item);

        return productColors;
    }
}
