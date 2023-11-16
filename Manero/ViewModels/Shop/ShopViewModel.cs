using Manero.Models.DTO;
namespace Manero.ViewModels.Shop;

public class ShopViewModel
{
    public IEnumerable<ProductModel> Items { get; set; } = null!; 
}

