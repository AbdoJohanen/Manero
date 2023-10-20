using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class TagService
{
    private readonly TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<TagModel>> GetAllTagsAsync()
    {
        var items = await _tagRepository.GetAllAsync();
        if (items != null)
        {
            var tags = new List<TagModel>();
            foreach (var item in items)
                tags.Add(item);

            return tags;
        }

        return null!;
    }
}
