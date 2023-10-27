using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class TagRepository : DataRepository<TagEntity>
{
    public TagRepository(DataContext context) : base(context)
    {
    }
}
