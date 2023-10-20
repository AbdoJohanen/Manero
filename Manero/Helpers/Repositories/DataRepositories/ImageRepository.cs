using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ImageRepository : DataRepository<ImageEntity>
{
    public ImageRepository(DataContext context) : base(context)
    {
    }
}
