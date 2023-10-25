using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ProductColorModel
    {
        public string ArticleNumber { get; set; } = null!;
        public int ColorId { get; set; }

        public static implicit operator ProductColorEntity(ProductColorModel model)
        {
            return new ProductColorEntity
            {
                ArticleNumber = model.ArticleNumber,
                ColorId = model.ColorId
            };
        }
    }
}
