using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal? ProductDiscount { get; set; }

    // ICollections go under here
    public ICollection<ProductCategoryEntity> ProductCategories { get; set; } = new HashSet<ProductCategoryEntity>();
    public ICollection<ProductTagEntity> ProductTags { get; set; } = new HashSet<ProductTagEntity>();
    public ICollection<ProductImageEntity> ProductImages { get; set; } = new HashSet<ProductImageEntity>();
}
