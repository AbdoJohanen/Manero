using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string Category { get; set; } = null!;
    public ICollection<ProductCategoryEntity> CategoryProducts { get; set; } = new HashSet<ProductCategoryEntity>();
}
