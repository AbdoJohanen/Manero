using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class CategoryRepository : DataRepository<CategoryEntity>
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
}
