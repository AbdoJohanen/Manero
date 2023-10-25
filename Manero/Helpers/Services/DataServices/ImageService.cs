using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Services.DataServices;

public class ImageService
{  
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ImageRepository _imageRepository;

    public ImageService(IWebHostEnvironment webHostEnvironment, ImageRepository imageRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _imageRepository = imageRepository;
    }


    // Saves the Image To Disk, And Creates A DataBase Record.
    public async Task<ImageModel> SaveProductImageAsync(ProductModel product, IFormFile file, bool isMainImage)
    {
        try
        {
            ImageEntity imageEntity = new ImageEntity();
            string filePath = $"{_webHostEnvironment.WebRootPath}/assets/images/products/{product.ArticleNumber + '_' + imageEntity.Id + '_' + file.FileName}";
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            if (isMainImage)
            {
                imageEntity.IsMainImage = true;
            }
            imageEntity.ProductArticleNumber = product.ArticleNumber;
            imageEntity.ImageUrl = filePath;

            return await _imageRepository.AddAsync(imageEntity);
        } catch
        {
            return null!;
        }
    }
}
