﻿using Manero.Helpers.Repositories.DataRepositories;
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

    public async Task<TagModel> GetTagAsync(int tagId)
    {
        if (tagId != 0)
            return await _tagRepository.GetAsync(x => x.Id == tagId);

        return null!;
    }

    public async Task<IEnumerable<TagModel>> GetTagsAsync(List<int> tagsId)
    {
        if (tagsId != null)
        {
            var tags = new List<TagModel>();
            foreach (var id in tagsId)
            {
                var items = await _tagRepository.GetAllAsync(x => x.Id == id);
                foreach (var tag in items)
                    tags.Add(tag);
            }

            return tags;
        }

        return null!;
    }
}
