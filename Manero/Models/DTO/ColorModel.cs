using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class ColorModel
    {
        public int Id { get; set; }
        public string Color { get; set; } = null!;

        public static implicit operator ColorEntity(ColorModel model)
        {
            return new ColorEntity
            {
                Id = model.Id,
                Color = model.Color
            };
        }
    }
}
