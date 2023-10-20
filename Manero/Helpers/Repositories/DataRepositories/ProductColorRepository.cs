using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductColorRepository : DataRepository<ProductColorEntity>
{
    public ProductColorRepository(DataContext context) : base(context)
    {
    }
}
