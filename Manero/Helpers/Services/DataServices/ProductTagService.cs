using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class ProductTagService
{
    private readonly ProductTagRepository _productTagRepository;
    private readonly DataContext _context;

    public ProductTagService(ProductTagRepository productTagRepository, DataContext context)
    {
        _productTagRepository = productTagRepository;
        _context = context;
    }
}
