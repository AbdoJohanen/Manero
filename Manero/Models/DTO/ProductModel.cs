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

        public decimal? CalculatedPrice => ProductPrice * (ProductDiscount / 100);
        

        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
        public List<SizeModel> Sizes { get; set; } = new List<SizeModel>();
        public List<ColorModel> Colors { get; set; } = new List<ColorModel>();
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();

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