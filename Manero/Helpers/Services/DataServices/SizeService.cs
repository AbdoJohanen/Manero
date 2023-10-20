using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class SizeService
{
    private readonly SizeRepository _sizeRepository;

    public SizeService(SizeRepository sizeRepository)
    {
        _sizeRepository = sizeRepository;
    }
}
