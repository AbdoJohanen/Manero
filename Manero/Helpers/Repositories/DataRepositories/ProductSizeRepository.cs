using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductSizeRepository : DataRepository<ProductSizeEntity>
{
    public ProductSizeRepository(DataContext context) : base(context)
    {
    }
}
