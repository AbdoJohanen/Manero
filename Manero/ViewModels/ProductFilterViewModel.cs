using Manero.Models.DTO;

namespace Manero.ViewModels;

public class ProductFilterViewModel
{
    public List<SizeModel> AvailableSizes { get; set; } = null!;
    public List<ColorModel> AvailableColors { get; set; } = null!;
    public List<TagModel> AvailableTags { get; set; } = null!;
    public List<CategoryModel> AvailableCategories { get; set; } = null!;
    public string[] Sizes { get; set; } = null!;
    public string[] Colors { get; set; } = null!;
    public string[] Tags { get; set; } = null!;
    public string[] Categories { get; set; } = null!;
    public decimal? MinPrice { get; set; } = 0;
    public decimal? MaxPrice { get; set; } = 10000;


}
