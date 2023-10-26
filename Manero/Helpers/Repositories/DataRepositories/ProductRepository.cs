using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductRepository : DataRepository<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context)
    {
    }
}
