using Manero.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities;

public class SizeEntity
{
    [Key]
    public int Id { get; set; }
    public string Size { get; set; } = null!;

    public ICollection<ProductSizeEntity> SizeProducts { get; set; } = new HashSet<ProductSizeEntity>();

    public static implicit operator SizeModel(SizeModel entity)
    {
        return new SizeModel
        {
            Id = entity.Id,
            Size = entity.Size
        };
    }
}
