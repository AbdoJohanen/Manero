using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ImageModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ImageUrl { get; set; } = null!;

        public static implicit operator ImageEntity(ImageModel model)
        {
            return new ImageEntity
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl
            };
        }
    }
}
