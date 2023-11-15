using Manero.Contexts;
using Manero.Models.Entities.UserEntities;

namespace Manero.Helpers.Repositories.UserRepositories;

public class UserAddressRepository : IdRepository<UserAddressEntity, IdentityContext>
{
    public UserAddressRepository(IdentityContext context) : base(context)
    {
    }
}