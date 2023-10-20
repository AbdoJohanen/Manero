using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class ImageService
{  
    private readonly ImageRepository _imageRepository;

    public ImageService(ImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
}
