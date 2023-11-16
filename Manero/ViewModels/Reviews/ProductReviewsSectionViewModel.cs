using Manero.Models.DTO;
using Manero.Models.Identity;

namespace Manero.ViewModels.Reviews;

public class ProductReviewsSectionViewModel
{
    public string SectionTitle { get; set; } = "Reviews and Ratings";
    public ProductModel Product { get; set; } = null!;
}
