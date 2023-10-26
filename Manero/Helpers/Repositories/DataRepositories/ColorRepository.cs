using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ColorRepository : DataRepository<ColorEntity>
{
    public ColorRepository(DataContext context) : base(context)
    {
    }
}
