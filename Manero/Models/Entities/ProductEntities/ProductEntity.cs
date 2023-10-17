using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class ProductEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
