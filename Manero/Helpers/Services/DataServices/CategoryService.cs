using Manero.Helpers.Repositories.DataRepositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manero.Helpers.Services.DataServices
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoryService(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            var categories = new List<SelectListItem>();

            foreach (var category in await _categoryRepo.GetAllAsync())
            {
                categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Category,
                });
            }
            return categories;
        }

        public async Task<List<SelectListItem>> GetCategoriesAsync(string[] selectedCategories)
        {
            var categories = new List<SelectListItem>();

            foreach (var category in await _categoryRepo.GetAllAsync())
            {
                categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Category,
                    Selected = selectedCategories.Contains(category.Id.ToString())
                });
            }
            return categories;
        }
    }
}
