using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.ViewModels
{
    public class ProductDetailsViewModel
    {
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductDiscount { get; set; }


        public List<SizeModel> Sizes { get; set; } = new List<SizeModel>();

        public GridCollectionViewModel All { get; set; } = null!;

    }
}
