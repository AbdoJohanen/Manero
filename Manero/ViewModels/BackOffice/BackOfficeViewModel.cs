using Manero.Models.DTO;

namespace Manero.ViewModels.BackOffice;

public class BackOfficeViewModel
{
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}
