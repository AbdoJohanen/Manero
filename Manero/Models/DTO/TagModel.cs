using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Tag { get; set; } = null!;

        public static implicit operator TagEntity(TagModel model)
        {
            return new TagEntity
            {
                Id = model.Id,
                Tag = model.Tag
            }
        }
    }
}
