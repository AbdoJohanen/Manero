using Manero.Models.Entities.ProductEntities;

namespace Manero.Models.DTO
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        
        public static implicit operator CategoryEntity(CategoryModel model)
        {
            return new CategoryEntity
            {
                Id = model.Id,
                Category = model.Category,
            };
        }
    }
}
