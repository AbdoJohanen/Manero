using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.ViewModels
{
    public class GridCollectionViewModel
    {
        public string Title { get; set; } = "";
        public IEnumerable<ProductModel> GridItems { get; set; } = null!;
        public string LoadMore { get; set; } = "";
    }
}
