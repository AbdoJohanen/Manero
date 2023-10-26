using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(SizeId), nameof(ArticleNumber))]
public class ProductSizeEntity
{
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public int SizeId { get; set; }
    public SizeEntity Size { get; set; } = null!;

    public static implicit operator ProductSizeModel(ProductSizeEntity entity)
    {
        return new ProductSizeModel
        {
            ArticleNumber = entity.ArticleNumber,
            SizeId = entity.SizeId
        };
    }

}
