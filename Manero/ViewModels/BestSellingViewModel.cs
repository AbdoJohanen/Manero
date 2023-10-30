using Manero.Models.DTO;

namespace Manero.ViewModels
{
    public class BestSellingViewModel
    {
        public string Title { get; set; } = "";
        public IEnumerable<ProductModel> GridItems { get; set; } = null!;
    }
}
