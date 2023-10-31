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
