﻿using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;
using Manero.Models.Entities.ProductEntities;
using Microsoft.IdentityModel.Tokens;

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
            var fileName = imageEntity.Id + file.FileName;
            string filePath = $"{_webHostEnvironment.WebRootPath}/assets/images/products/{fileName}";
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            if (isMainImage)
            {
                imageEntity.IsMainImage = true;
            }
            imageEntity.ProductArticleNumber = product.ArticleNumber;
            imageEntity.ImageUrl = fileName;

            return await _imageRepository.AddAsync(imageEntity);
        } catch
        {
            return null!;
        }
    }

    public async Task<ImageModel> GetMainImageAsync(string articleNumber)
    {
        if (!articleNumber.IsNullOrEmpty())
            return await _imageRepository.GetAsync(x => x.ProductArticleNumber == articleNumber && x.IsMainImage == true);

        return null!;
    }
}
