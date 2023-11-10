using Manero.Contexts;
using Manero.Models.Entities.UserEntities;

namespace Manero.Helpers.Repositories.UserRepositories;

public class AddressRepository : IdRepository<AddressEntity>
{
    public AddressRepository(IdentityContext context) : base(context)
    {
    }
}
