using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class SizeModel
    {
        public int Id { get; set; }
        public string Size { get; set; } = null!;

        public static implicit operator SizeEntity(SizeModel model)
        {
            return new SizeEntity
            {
                Id = model.Id,
                Size = model.Size
            };
        }
    }
}
