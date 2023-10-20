using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class ColorService
{

    private readonly ColorRepository _colorRepository;

    public ColorService(ColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }
}
