using Manero.Models.DTO;

namespace Manero.ViewModels;

public class ProductFilterViewModel
{
    public string[] Sizes { get; set; } = null!;
    public string[] Colors { get; set; } = null!;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string[] Tags { get; set; } = null!;
    public string[] Categories { get; set; } = null!;
}
