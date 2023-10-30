using Manero.Models.DTO;
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

    public ICollection<ProductColorEntity> ProductColors { get; set; } = new HashSet<ProductColorEntity>();
    public ICollection<ProductSizeEntity> ProductSizes { get; set; } = new HashSet<ProductSizeEntity>();
    public ICollection<ReviewEntity> ProductReviews { get; set; } = new HashSet<ReviewEntity>();
    public ICollection<ProductCategoryEntity> ProductCategories { get; set; } = new HashSet<ProductCategoryEntity>();
    public ICollection<ProductTagEntity> ProductTags { get; set; } = new HashSet<ProductTagEntity>();
    public ICollection<ImageEntity> Images { get; set; } = new HashSet<ImageEntity>();

    public static implicit operator ProductModel(ProductEntity entity)
    {
        return new ProductModel
        {
            ArticleNumber = entity.ArticleNumber,
            ProductName = entity.ProductName,
            ProductDescription = entity.ProductDescription!,
            ProductPrice = entity.ProductPrice,
            ProductDiscount = entity.ProductDiscount,

        };
    }
}
