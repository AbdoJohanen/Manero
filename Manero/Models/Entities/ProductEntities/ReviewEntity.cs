using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(Id))]
public class ReviewEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Rating { get; set; }
    public string? Review { get; set; }

    [ForeignKey(nameof(ArticleNumber))]
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
}
