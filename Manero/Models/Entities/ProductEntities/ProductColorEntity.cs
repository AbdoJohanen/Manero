using Microsoft.EntityFrameworkCore;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(ColorId), nameof(ArticleNumber))]
public class ProductColorEntity
{
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public int ColorId { get; set; }
    public ColorEntity Color { get; set; } = null!;

}
