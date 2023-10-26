using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ReviewRepository : DataRepository<ReviewEntity>
{
    public ReviewRepository(DataContext context) : base(context)
    {
    }
}
