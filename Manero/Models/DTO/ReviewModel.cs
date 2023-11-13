using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO;

public class ReviewModel
{
    public string? Reviewer { get; set; }
    public string Comment { get; set; } = null!;
    public int Rating { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public static implicit operator ReviewEntity(ReviewModel model)
    {
        return new ReviewEntity
        {
            Reviewer = model.Reviewer,
            Comment = model.Comment,
            Rating = model.Rating,
            ArticleNumber = model.ArticleNumber
        };
    }
}
