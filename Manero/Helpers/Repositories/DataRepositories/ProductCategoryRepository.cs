using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductCategoryRepository : DataRepository<ProductCategoryEntity>
{
    public ProductCategoryRepository(DataContext context) : base(context)
    {
    }
}
