using Manero.Models.DTO;

namespace Manero.ViewModels
{
    public class HomeIndexViewModel
    {
        public GridCollectionViewModel Featured { get; set; } = null!;
        public BestSellingViewModel BestSelling { get; set; } = null!;
        public IEnumerable<CategoryModel>? Categories { get; set; }
    }
}
