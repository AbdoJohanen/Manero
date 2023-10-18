namespace Manero.Models.Entities.ProductEntities
{
    public class ProductReviewsEntity
    {

        public Guid Id { get; set; }

        public int Rating { get; set; }
        public string? Review { get; set; }

        public string ArticleNumber { get; set; } = null!;
        public ProductEntity Product { get; set; } = null!;

    }
}
