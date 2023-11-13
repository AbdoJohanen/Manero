using Manero.Contexts;
using Manero.Models.Identity;

namespace Manero.Helpers.Repositories.UserRepositories;

public class UserRepository : IdRepository<AppUser>
{
    public UserRepository(IdentityContext context) : base(context)
    {
    }
}
