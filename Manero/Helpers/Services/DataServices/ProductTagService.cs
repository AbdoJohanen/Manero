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


    // First gets list of ProductTagModel by articleNumber
    // Then removing the existing TagIds 
    // Then creates new ProductTagModel by looping thru tags
    public async Task<IEnumerable<ProductTagModel>> UpdateProductTagsAsync(string articleNumber, List<int> tags)
    {
        var productTags = new List<ProductTagModel>();
        var existingTags = await _productTagRepository.GetAllAsync(x => x.ArticleNumber == articleNumber);

        if (existingTags != null)
        {
            foreach (var tag in existingTags)
                await _productTagRepository.DeleteAsync(tag);

            foreach (var tagId in tags)
            {
                var updatedProductTag = await _productTagRepository.AddAsync(new ProductTagModel { ArticleNumber = articleNumber, TagId = tagId });
                productTags.Add(updatedProductTag);
            }
                
            return productTags;
        }

        return null!;
    }
}
