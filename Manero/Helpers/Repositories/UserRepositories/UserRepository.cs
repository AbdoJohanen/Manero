using Manero.Contexts;
using Manero.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Manero.Helpers.Repositories.UserRepositories;

public class UserRepository : IdRepository<AppUser, IdentityContext>
{
    public UserRepository(IdentityContext context) : base(context)
    {
    }
}
