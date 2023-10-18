namespace Manero.Models.Entities.ProductEntities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Category { get; set; } = null!;
    public ICollection<ProductCategoryEntity> CategoryProducts { get; set; } = new HashSet<ProductCategoryEntity>();
}
