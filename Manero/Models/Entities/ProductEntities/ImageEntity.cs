using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(Id))]
public class ImageEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ImageUrl { get; set; } = null!;

    [ForeignKey(nameof(ArticleNumber))]
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public static implicit operator ImageModel(ImageEntity entity)
    {
        return new ImageEntity
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl
        };
    }
}
