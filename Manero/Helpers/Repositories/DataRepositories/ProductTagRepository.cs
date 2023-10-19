using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductTagRepository : DataRepository<ProductTagEntity>
{
    public ProductTagRepository(DataContext context) : base(context)
    {
    }
}
