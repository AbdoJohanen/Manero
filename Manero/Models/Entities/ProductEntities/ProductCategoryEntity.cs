using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(ArticleNumber), nameof(CategoryId))]
public class ProductCategoryEntity
{
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    public static implicit operator ProductCategoryModel(ProductCategoryEntity entity)
    {
        return new ProductCategoryModel
        {
            ArticleNumber = entity.ArticleNumber,
            CategoryId = entity.CategoryId,
        };
    }
}
