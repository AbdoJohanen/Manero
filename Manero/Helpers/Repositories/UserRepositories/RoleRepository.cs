using Manero.Contexts;
using Manero.Models.Identity;

namespace Manero.Helpers.Repositories.UserRepositories;

public class RoleRepository : IdRepository<AppUser>
{
    public RoleRepository(IdentityContext context) : base(context)
    {
    }
}
