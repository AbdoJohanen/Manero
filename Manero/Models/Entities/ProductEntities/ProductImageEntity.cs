using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(Id))]
public class ProductImageEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ImageUrl { get; set; } = null!;

    [ForeignKey(nameof(ArticleNumber))]
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
}
