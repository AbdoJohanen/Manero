using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

namespace Manero.Helpers.Services.DataServices;

public class CategoryService
{

    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
    {
        var items = await _categoryRepository.GetAllAsync();
        if(items != null)
        {
            var categories = new List<CategoryModel>();
            foreach (var item in items)
                categories.Add(item);

            return categories;
        }
        return null!;
    }

    public async Task<List<string>> GetAllCategoriesNamesToStringAsync()
    {
        var items = await _categoryRepository.GetAllAsync();
        if (items != null)
        {
            var categories = new List<string>();
            foreach (var item in items)
                categories.Add(item.Category);

            return categories;
        }
        return null!;
    }
    public async Task<IEnumerable<CategoryModel>> GetAllCategoriesToModelAsync()
    {
        var categoriesEntities = await _categoryRepository.GetAllAsync();

        var categoriesModels = categoriesEntities.Select(categoryEntity => new CategoryModel
        {
            Id = categoryEntity.Id,
            Category = categoryEntity.Category,
        }).ToList();

        return categoriesModels;
    }


    // Gets specific category based on Category Id
    public async Task<CategoryModel> GetCategoryAsync(int categoryId)
    {
        if (categoryId != 0)
            return await _categoryRepository.GetAsync(x => x.Id == categoryId);

        return null!;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(List<int> categoriesId)
    {
        if (categoriesId != null)
        {
            var categories = new List<CategoryModel>();
            foreach (var id in categoriesId)
            {
                var items = await _categoryRepository.GetAllAsync(x => x.Id == id);
                foreach (var category in items)
                    categories.Add(category);
            }

            return categories;
        }

        return null!;
    }

}
