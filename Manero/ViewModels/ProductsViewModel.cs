using Manero.Models.DTO;

namespace Manero.ViewModels;

public class ProductsViewModel
{
    public List<ProductModel> Products { get; set; } = null!;
    public string CurrentFilter { get; set; } = null!;
    public ProductFilterViewModel ProductFilters { get; set; } = null!;
}
