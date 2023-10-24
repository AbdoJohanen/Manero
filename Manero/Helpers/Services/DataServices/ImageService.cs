using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.Entities.ProductEntities;

namespace Manero.Helpers.Services.DataServices
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;

        public ImageService(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<IEnumerable<ImageEntity>> GetAllAsync()
        {
            var Images = (await _imageRepository.GetAllAsync())
                .Select(Image => new ImageEntity
                {
                    Id = Image.Id.ToString(),
                    ImageUrl = Image.ImageUrl
                })
                .ToList();

            return Images;
        }
        //private readonly ImageRepository imageRepository;

        //public ImageService(ImageRepository imageRepository)
        //{
        //    this.imageRepository = imageRepository;
        //}

        //public async Task<IEnumerable<ImageEntity>> GetAllAsync()
        //{
        //    var Images = new List<ImageEntity>();
        //    foreach(var Image in await imageRepository.GetAllAsync())
        //    {
        //        Images.Add(new ImageEntity
        //        {
        //            Id = Image.Id.ToString(),
        //            ImageUrl = Image.ImageUrl
        //        });
        //    }
        //    return Images;

        //}
    }
}
