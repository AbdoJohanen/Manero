using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(Id))]
public class ImageEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ImageUrl { get; set; } = null!;

    [ForeignKey("Product")]
    public string ProductArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public bool IsMainImage { get; set; } = false;

    public static implicit operator ImageModel(ImageEntity entity)
    {
        return new ImageModel
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            IsMainImage = entity.IsMainImage,
            ProductArticleNumber = entity.ProductArticleNumber,
            Product = entity.Product,
        };
    }
}
