using System.ComponentModel.DataAnnotations;

namespace Manero.Models.Entities.ProductEntities
{
    public class SizeEntity
    {
        [Key]
        public int Id { get; set; }
        public string Size { get; set; } = null!;

        public ICollection<ProductSizeEntity> Sizes { get; set; } = new HashSet<ProductSizeEntity>();
    }
}
