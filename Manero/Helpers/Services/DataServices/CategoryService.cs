using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class CategoryService
{

    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


}
