using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductTagService
{
    private readonly ProductTagRepository _productTagRepository;

    public ProductTagService(ProductTagRepository productTagRepository)
    {
        _productTagRepository = productTagRepository;
    }

    // Takes a list of int (selected tags) and a ProductModel, than sends a new ProductTag to the repository
    public async Task AssociateTagsWithProductAsync(IEnumerable<TagModel> selectedTags, ProductModel product)
    {
        if (selectedTags.Any() && product != null)
        {
            var articleNumber = product.ArticleNumber;
            foreach (var tag in selectedTags) 
            { 
                await _productTagRepository.AddAsync(new ProductTagModel
                {
                    ArticleNumber = articleNumber,
                    Product = product,
                    TagId = tag.Id,
                    Tag = tag
                });
            }
        }
    }

    // Gets a list of ProductTagModel from repository
    public async Task<IEnumerable<ProductTagModel>> GetProductWithTagsAsync()
    {
        var productTags = new List<ProductTagModel>();
        foreach (var item in await _productTagRepository.GetAllAsync())
            productTags.Add(item);

        return productTags;
    }
}
