using Manero.Contexts;
using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;

namespace Manero.Helpers.Repositories.UserRepositories;

public interface IUserAddressRepository : IRepo<UserAddressEntity, IdentityContext>
{
}

public class UserAddressRepository : IdRepository<UserAddressEntity, IdentityContext> , IUserAddressRepository
{
    public UserAddressRepository(IdentityContext context) : base(context)
    {
    }
}