using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ProductTagModel
    {
        public string ArticleNumber { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;

        public int TagId { get; set; }
        public TagModel Tag { get; set; } = null!;

        public static implicit operator ProductTagEntity(ProductTagModel model)
        {
            return new ProductTagEntity
            {
                ArticleNumber = model.ArticleNumber,
                TagId = model.TagId
            };
        }
    }
}
