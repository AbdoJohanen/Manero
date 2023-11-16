using Manero.Contexts;
using Manero.Models.Entities.UserEntities;

namespace Manero.Helpers.Repositories.UserRepositories;

public interface IAddressRepository : IRepo<AddressEntity, IdentityContext>
{
}
public class AddressRepository : IdRepository<AddressEntity, IdentityContext> , IAddressRepository
{
    public AddressRepository(IdentityContext context) : base(context)
    {
    }
}
