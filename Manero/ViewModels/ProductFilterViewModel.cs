using Manero.Models.DTO;

namespace Manero.ViewModels;

public class ProductFilterViewModel
{
    public List<string> AvailableSizes { get; set; } = null!;
    public List<string> AvailableColors { get; set; } = null!;
    public List<string> AvailableTags { get; set; } = null!;
    public List<string> AvailableCategories { get; set; } = null!;
    public string[] Sizes { get; set; } = null!;
    public string[] Colors { get; set; } = null!;
    public string[] Tags { get; set; } = null!;
    public string[] Categories { get; set; } = null!;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }


}
