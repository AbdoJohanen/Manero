using Manero.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class ColorEntity
{
    [Key]
    public int Id { get; set; }
    public string Color { get; set; } = null!;

    public ICollection<ProductColorEntity> ColorsProducts { get; set; } = new HashSet<ProductColorEntity>();

    public static implicit operator ColorModel(ColorEntity entity)
    {
        return new ColorModel
        {
            Id = entity.Id,
            Color = entity.Color
        };
    }
}
