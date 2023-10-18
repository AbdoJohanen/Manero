using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class TagEntity
{
    [Key]
    public int Id { get; set; }
    public string Tag { get; set; } = null!;
    public ICollection<ProductTagEntity> TagProducts { get; set; } = new HashSet<ProductTagEntity>();
}
