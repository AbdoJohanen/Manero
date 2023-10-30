using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ProductCategoryModel
    {
        public string ArticleNumber { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; } = null!;

        public static implicit operator ProductCategoryEntity(ProductCategoryModel model)
        {
            return new ProductCategoryEntity
            {
                ArticleNumber = model.ArticleNumber,
                CategoryId = model.CategoryId,
            };
        }
    }
}
