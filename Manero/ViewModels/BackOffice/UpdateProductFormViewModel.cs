using Manero.Models.DTO;
using System.ComponentModel;

namespace Manero.ViewModels.BackOffice;

public class UpdateProductFormViewModel
{
    public ProductModel Product { get; set; } = new ProductModel();

    [DisplayName("Product Name")]
    public string? ProductName { get; set; }

    [DisplayName("Product Description")]
    public string? ProductDescription { get; set; }

    [DisplayName("Product Price")]
    public decimal? ProductPrice { get; set; }

    [DisplayName("Product Discount")]
    public decimal? ProductDiscount { get; set; }

    [DisplayName("Tags")]
    public List<TagModel> Tags { get; set; } = new List<TagModel>();
    public List<int> CurrentTags { get; set; } = new List<int>();
    public List<int>? SelectedTags { get; set; } = new List<int>();

    [DisplayName("Categories")]
    public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    public List<int> CurrentCategories { get; set; } = new List<int>();
    public List<int>? SelectedCategories { get; set; } = new List<int>();

    [DisplayName("Colors")]
    public List<ColorModel> Colors { get; set; } = new List<ColorModel>();
    public List<int> CurrentColors { get; set; } = new List<int>();
    public List<int>? SelectedColors { get; set; } = new List<int>();

    [DisplayName("Sizes")]
    public List<SizeModel> Sizes { get; set; } = new List<SizeModel>();
    public List<int> CurrentSizes { get; set; } = new List<int>();
    public List<int>? SelectedSizes { get; set; } = new List<int>();

    public static implicit operator ProductModel(UpdateProductFormViewModel viewModel)
    {
        return new ProductModel
        {
            ProductName = viewModel.ProductName!,
            ProductDescription = viewModel.ProductDescription,
            ProductPrice = viewModel.ProductPrice ?? 0,
            ProductDiscount = viewModel.ProductDiscount
        };
    }
}
