using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ImageModel
    {
        public string Id { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string ProductArticleNumber { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
        public bool IsMainImage { get; set; } = false;

        public static implicit operator ImageEntity(ImageModel model)
        {
            return new ImageEntity
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                IsMainImage = model.IsMainImage,
                ProductArticleNumber = model.ProductArticleNumber,
                Product = model.Product,
            };
        }
    }
}
