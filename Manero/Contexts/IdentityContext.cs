using Manero.Models.Entities.UserEntities;
using Manero.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Manero.Contexts;

public class IdentityContext : IdentityDbContext<AppUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected IdentityContext()
    {
    }


    public DbSet<AddressEntity> AspNetAddresses { get; set; }
    public DbSet<UserAddressEntity> AspNetUsersAddresses { get; set; }
}
