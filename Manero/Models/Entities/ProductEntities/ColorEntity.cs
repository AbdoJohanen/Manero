using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class ColorEntity
{
    [Key]
    public int Id { get; set; }
    public string Color { get; set; } = null!;

    public ICollection<ProductColorEntity> ColorsProducts { get; set; } = new HashSet<ProductColorEntity>();
}
