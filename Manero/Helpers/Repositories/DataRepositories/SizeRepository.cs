using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class SizeRepository : DataRepository<SizeEntity>
{
    public SizeRepository(DataContext context) : base(context)
    {
    }
}
