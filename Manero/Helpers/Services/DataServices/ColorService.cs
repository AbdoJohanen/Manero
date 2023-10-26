using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;
using System.Drawing;

namespace Manero.Helpers.Services.DataServices;

public class ColorService
{

    private readonly ColorRepository _colorRepository;

    public ColorService(ColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<IEnumerable<ColorModel>> GetAllColorsAsync()
    {
        var items = await _colorRepository.GetAllAsync();
        if (items != null)
        {
            var colors = new List<ColorModel>();
            foreach (var item in items)
                colors.Add(item);

            return colors;
        }

        return null!;
    }

    // Gets specific color based on Color Id
    public async Task<ColorModel> GetColorAsync(int ColorId)
    {
        if (ColorId != 0)
            return await _colorRepository.GetAsync(x => x.Id == ColorId);

        return null!;
    }

    public async Task<IEnumerable<ColorModel>> GetColorsAsync(List<int> colorsId)
    {
        if (colorsId != null)
        {
            var colors = new List<ColorModel>();
            foreach (var id in colorsId)
            {
                var items = await _colorRepository.GetAllAsync(x => x.Id == id);
                foreach (var color in items)
                    colors.Add(color);
            }

            return colors;
        }

        return null!;
    }
}
