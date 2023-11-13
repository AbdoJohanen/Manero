using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class SizeService
{
    private readonly SizeRepository _sizeRepository;

    public SizeService(SizeRepository sizeRepository)
    {
        _sizeRepository = sizeRepository;
    }
    // Gets all tags as TagModel from repository
    public async Task<IEnumerable<SizeModel>> GetAllSizesAsync()
    {
        var items = await _sizeRepository.GetAllAsync();
        if (items != null)
        {
            var sizes = new List<SizeModel>();
            foreach (var item in items)
                sizes.Add(item);

            return sizes;
        }

        return null!;
    }



    // Gets specific SizeModel from repository
    public async Task<SizeModel> GetSizeAsync(int sizeId)
    {
        if (sizeId != 0)
            return await _sizeRepository.GetAsync(x => x.Id == sizeId);

        return null!;
    }

    // Gets a list of SizeModel with a expression (List<int> sizesId) that comes from view
    public async Task<IEnumerable<SizeModel>> GetSizesAsync(List<int> sizesId)
    {
        if (sizesId != null)
        {
            var sizes = new List<SizeModel>();
            foreach (var id in sizesId)
            {
                var items = await _sizeRepository.GetAllAsync(x => x.Id == id);
                foreach (var size in items)
                    sizes.Add(size);
            }

            return sizes;
        }

        return null!;
    }
}
