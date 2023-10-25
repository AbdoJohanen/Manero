using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ProductSizeModel
    {
        public string ArticleNumber { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;

        public int SizeId { get; set; }
        public SizeModel Size { get; set; } = null!;

        public static implicit operator ProductSizeEntity(ProductSizeModel model)
        {
            return new ProductSizeEntity
            {
                ArticleNumber = model.ArticleNumber,
                SizeId = model.SizeId,
            };
        }
    }
}
