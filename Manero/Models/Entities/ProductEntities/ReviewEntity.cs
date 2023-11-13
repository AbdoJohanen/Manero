using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Models.Entities.ProductEntities;

[PrimaryKey(nameof(Id))]
public class ReviewEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Reviewer { get; set; }
    public string Comment { get; set; } = null!;
    public int Rating { get; set; }

    [ForeignKey(nameof(ArticleNumber))]
    public string ArticleNumber { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    public static implicit operator ReviewModel(ReviewEntity entity)
    {
        return new ReviewModel
        {
            Reviewer = entity.Reviewer,
            Comment = entity.Comment,
            Rating = entity.Rating,
            ArticleNumber = entity.ArticleNumber
        };
    }
}
