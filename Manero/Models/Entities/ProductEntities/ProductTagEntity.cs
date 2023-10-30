using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(ArticleNumber), nameof(TagId))]
public class ProductTagEntity
{
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public int TagId { get; set; }
    public TagEntity Tag { get; set; } = null!;

    public static implicit operator ProductTagModel(ProductTagEntity entity)
    {
        return new ProductTagModel
        {
            ArticleNumber = entity.ArticleNumber,
            TagId = entity.TagId
        };
    }
}
