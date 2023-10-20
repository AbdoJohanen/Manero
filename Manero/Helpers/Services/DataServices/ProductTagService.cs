using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class ProductTagService
{
    private readonly ProductTagRepository _productTagRepository;
    private readonly ProductService _productService;
    private readonly TagService _tagService;

    public ProductTagService(ProductTagRepository productTagRepository, ProductService productService, TagService tagService)
    {
        _productTagRepository = productTagRepository;
        _productService = productService;
        _tagService = tagService;
    }

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
}
