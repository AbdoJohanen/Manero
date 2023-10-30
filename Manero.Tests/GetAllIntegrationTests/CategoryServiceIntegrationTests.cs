using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Manero.Tests
{
    public class CategoryServiceIntegrationTests
    {
        private readonly CategoryService _service;
        private readonly CategoryRepository _repository;
        private readonly DataContext _context;

        public CategoryServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repository = new CategoryRepository(_context);
            _service = new CategoryService(_repository);
        }

        [Fact]
        public async Task GetAllCategoriesToModelAsync_ShouldReturnCategoriesInModelFormat()
        {
            // Arrange: Lägg till testdata i din InMemory-databas
            var category1 = new CategoryModel { Id = 1, Category = "Category 1" };
            var category2 = new CategoryModel { Id = 2, Category = "Category 2" };

            await _context.Categories.AddRangeAsync(category1, category2);
            await _context.SaveChangesAsync();

            // Act: Anropa GetAllCategoriesToModelAsync-metoden
            var result = await _service.GetAllCategoriesToModelAsync();

            // Assert: Kontrollera att resultatet är en lista med kategorimodeller i rätt format
            Assert.NotNull(result);
            Assert.IsType<List<CategoryModel>>(result);

            // Kontrollera att antalet kategorimodeller matchar antalet kategorier i databasen
            Assert.Equal(2, result.Count());

            // Rensa InMemory-databasen efter testet om det behövs
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}