using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ProductModel
    {
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? ProductDiscount { get; set; }

        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
        public List<ImageModel> Images { get; set; } = null!;

        public static implicit operator ProductEntity(ProductModel model)
        {
            return new ProductEntity
            {
                ArticleNumber = model.ArticleNumber,
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription!,
                ProductPrice = model.ProductPrice,
                ProductDiscount = model.ProductDiscount
            };
        } 
    }
}