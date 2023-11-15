using Manero.Contexts;
using Manero.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Manero.Helpers.Repositories.UserRepositories;

public interface IUserRepository : IRepo<AppUser, IdentityContext>
{
}

public class UserRepository : IdRepository<AppUser, IdentityContext>, IUserRepository
{
    public UserRepository(IdentityContext context) : base(context)
    {
    }
}
